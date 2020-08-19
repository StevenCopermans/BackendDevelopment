using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBackend.Models;
using ProjectBackend.Models.Repositories;
using ProjectBackend.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace ProjectBackend.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IQuizRepo quizRepo;
        private readonly IDifficultyRepo difficultyRepo;
        private readonly IQuestionRepo questionRepo;
        private readonly IAnswerRepo answerRepo;

        public HomeController(ILogger<HomeController> logger, IQuizRepo quizRepo, IDifficultyRepo difficultyRepo, IQuestionRepo questionRepo, IAnswerRepo answerRepo)
        {
            _logger = logger;
            this.quizRepo = quizRepo;
            this.difficultyRepo = difficultyRepo;
            this.questionRepo = questionRepo;
            this.answerRepo = answerRepo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Quiz> quizes = await quizRepo.GetAllQuizesAsync();

            if (quizes is null)
            {
                return NotFound();
            }

            return View(quizes);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateQuiz()
        {
            IEnumerable<Difficulty> difficulties = await difficultyRepo.GetAllDifficultiesAsync();

            return View(difficulties);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TryCreateQuiz()
        {

            try
            {
                Quiz @quiz = new Quiz
                {
                    Label = Request.Form["Title"],
                    DifficultyID = Guid.Parse(Request.Form["Difficulty"]),
                    Description = Request.Form["Description"],
                    ID = Guid.NewGuid()
                };

                Quiz result = await quizRepo.Add(@quiz);

                if (result == null)
                {
                    throw new Exception();
                }

                ICollection<string> keys = Request.Form.Keys;

                List<string> questions = new List<string>();
                List<string> answers = new List<string>();
                List<string> radioButtons = new List<string>();

                foreach (string key in keys)
                {
                    Console.WriteLine(key);
                    Console.WriteLine(Request.Form[key]);
                    if (Regex.IsMatch(key, @"^q[1-9]+$"))
                    {
                        Console.WriteLine("AAAAAAAAAAHHHHHHHHHHHHHHHHH");
                        questions.Add(key);
                    }
                    else if (Regex.IsMatch(key, @"^q[1-9]+a[1-9]+$"))
                    {
                        answers.Add(key);
                    }
                    else if (Regex.IsMatch(key, @"^q[1-9]+Radio$"))
                    {
                        radioButtons.Add(key);
                    }
                }

                Console.WriteLine(questions.Count);

                for (int i = 0; i < questions.Count; ++i)
                {
                    Console.WriteLine("Going through questions");
                    string questionNumber = questions[i].Substring(1);
                    Question @question = new Question()
                    {
                        ID = Guid.NewGuid(),
                        Label = Request.Form[questions[i]],
                        QuizID = result.ID
                    };

                    Question questionResult = await questionRepo.Add(@question);

                    if (questionResult == null)
                    {
                        throw new Exception();
                    }

                    string correctAnswer = "99999";
                    for (int j = 0; j < radioButtons.Count; ++j)
                    {
                        if (Regex.IsMatch(radioButtons[j], @"^q" + questionNumber + @"Radio$"))
                        {
                            correctAnswer = Regex.Match(Request.Form[radioButtons[j]], @"[1-9]+", RegexOptions.RightToLeft).Value;
                            Console.WriteLine(correctAnswer);
                        }
                    }

                    for (int j = 0; j < answers.Count; ++j)
                    {
                        if (Regex.IsMatch(answers[j], @"^q" + questionNumber))
                        {
                            bool isCorrectAnswer = (Regex.Match(answers[j], @"[1-9]+", RegexOptions.RightToLeft).Value == correctAnswer);

                            Answer @answer = new Answer()
                            {
                                ID = Guid.NewGuid(),
                                Label = Request.Form[answers[j]],
                                CorrectAnswer = isCorrectAnswer,
                                QuestionID = questionResult.ID
                            };

                            Answer answerResult = await answerRepo.Add(@answer);

                            if (answerResult == null)
                            {
                                throw new Exception();
                            }
                        }
                    }
                }


                return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Create is unable to save");
                    return View("index");
               }
        }
    }
}
