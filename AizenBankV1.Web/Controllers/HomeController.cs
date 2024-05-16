using AizenBankV1.BusinessLogic;
using AizenBankV1.BusinessLogic.DBModel.Seed;
using AizenBankV1.BusinessLogic.Interfaces;
using AizenBankV1.Domain.Entities.Card;
using AizenBankV1.Domain.Entities.User;
using AizenBankV1.Web.AdminAttributes;
using AizenBankV1.Web.Extensions;
using AizenBankV1.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AizenBankV1.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISession _session;
        public HomeController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        private readonly UserContext _userContext;
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("LogIn", "Register");
            }
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(user);

            return View(cards);
        }


        public ActionResult Profile()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(user);
            double money = 0;
            foreach(CardMinimal card in cards)
            {
                money += card.MoneyAmount;
            }
            var profileInfo = new ProfileData
            {
                Email = user.Email,
                Name = user.Username,
                OpenCards = cards.Count(),
                Money = money
            };
            return View(profileInfo);
        }

        public ActionResult ActivityLog()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData userData = new UserData
            {
                Name = user.Username,
                Email = user.Email
            };

            return View(userData);
        }

        [HttpGet]
        public ActionResult CardsAccounts()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            return View(cards);
        }

        public ActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCard(CardMinimal cardInfo)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            if(cards.Count >= 4)
            {
                return RedirectToAction("CardsAccount", "Home");
            }

            _session.CreateCard(cardInfo, currentUser);
            return RedirectToAction("CardsAccounts", "Home");
        }

        public ActionResult Deposit()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);

            var depositDataModel = new DepositDataModel
            {
                UserCards = cards,
                Money = 0
            };
            return View(depositDataModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(DepositDataModel data)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            data.UserCards = _session.GetCards(currentUser);
            var selectedCard = data.UserCards.FirstOrDefault(c => c.Name == data.SelectedCardName);

            if (selectedCard != null)
            {
                if (selectedCard.Name == "[Blocked Card]")
                {
                    return RedirectToAction("Failed", "Home");
                }

                if (!decimal.TryParse(data.Money.ToString(), out decimal money) || money <= 0)
                {
                    ModelState.AddModelError("Money", "Amount must be greater than 0");
                }
                else
                {
                    var depositData = new DepositData
                    {
                        CardName = selectedCard.Name,
                        Money = data.Money
                    };
                    _session.Deposit(depositData);
                    return RedirectToAction("DepositSuccess", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("SelectedCardName", "Selected card not found.");
            }

            return View(data);
        }

        public ActionResult DepositSuccess()
        {
            return View();
        }

        public ActionResult Withdraw()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);

            var depositDataModel = new DepositDataModel
            {
                UserCards = cards,
                Money = 0
            };

            return View(depositDataModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(DepositDataModel data)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            data.UserCards = _session.GetCards(currentUser);
            var selectedCard = data.UserCards.FirstOrDefault(c => c.Name == data.SelectedCardName);

            if (selectedCard != null)
            {
                if (selectedCard.Name == "[Blocked Card]")
                {
                    return RedirectToAction("Failed", "Home");
                }


                if (!decimal.TryParse(data.Money.ToString(), out decimal money) || money <= 0)
                {
                    ModelState.AddModelError("Money", "Amount must be greater than 0");
                }
                else if (data.Money > selectedCard.MoneyAmount)
                {
                    ModelState.AddModelError("Money", "Insufficient balance for withdrawal.");
                }
                else
                {
                    var depositData = new DepositData
                    {
                        CardName = selectedCard.Name,
                        Money = data.Money
                    };
                    _session.Withdraw(depositData);
                    return RedirectToAction("WithdrawSucces", "Home");
                }
                
                
            }
            else
            {
                ModelState.AddModelError("SelectedCardName", "Selected card not found.");
            }

            return View(data);
        }

        public ActionResult WithdrawSucces()
        {
            return View();
        }

        public ActionResult LocalTransfer()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            var transferInfo = new LocalTransferModel
            {
                Cards = cards,
                Money = 0
            };
            return View(transferInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocalTransfer(LocalTransferModel transferInfo)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            transferInfo.Cards = _session.GetCards(currentUser);
            var sourceCard = transferInfo.Cards.FirstOrDefault(c => c.Name == transferInfo.SourceCard);
            var destinationCard = transferInfo.Cards.FirstOrDefault(c => c.Name == transferInfo.DestinationCard);

            if (sourceCard != null && destinationCard != null && (sourceCard != destinationCard))
            {
                if (sourceCard.Name == "[Blocked Card]" || destinationCard.Name == "[Blocked Card]")
                {
                    return RedirectToAction("Failed", "Home");
                }

                if (!decimal.TryParse(transferInfo.Money.ToString(), out decimal money) || money <= 0)
                {
                    ModelState.AddModelError("Money", "Amount must be greater than 0");
                }
                else if (transferInfo.Money > sourceCard.MoneyAmount)
                {
                    ModelState.AddModelError("Money", "Insufficient balance for transfer.");
                }
                else
                {
                    var transferData = new LocalTransferData
                    {
                        SourceCardName = sourceCard.Name,
                        DestinationCardName = destinationCard.Name,
                        Money = transferInfo.Money
                    };
                    _session.LocalTransfer(transferData);
                    return RedirectToAction("LocalTransferSucces", "Home");
                }


            }
            else
            {
                ModelState.AddModelError("SourceCard", "Selected card not found or cards cannot be the same");
            }

            return View(transferInfo);
        }

        public ActionResult LocalTransferSucces()
        {
            return View();
        }

        public ActionResult Transfer()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var cards = _session.GetCards(currentUser);
            var transferInfo = new TransferModel
            {
                Cards = cards,
                Money = 0
            };
            return View(transferInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(TransferModel transferInfo)
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            transferInfo.Cards = _session.GetCards(currentUser);
            var sourceCard = transferInfo.Cards.FirstOrDefault(c => c.Name == transferInfo.SourceCard);
            var destinationCard = transferInfo.DestinatorEmail;

            try
            {
                if (sourceCard != null && _session.UserExists(destinationCard))
                {
                    if (sourceCard.Name == "[Blocked Card]")
                    {
                        return RedirectToAction("Failed", "Home");
                    }

                    if (!decimal.TryParse(transferInfo.Money.ToString(), out decimal money) || money <= 0)
                    {
                        ModelState.AddModelError("Money", "Amount must be greater than 0");
                    }
                    else if (transferInfo.Money > sourceCard.MoneyAmount)
                    {
                        ModelState.AddModelError("Money", "Insufficient balance for transfer.");
                    }
                    else
                    {
                        var transferData = new TransferData
                        {
                            SourceCard = sourceCard.Name,
                            DestinationCard = destinationCard,
                            Money = transferInfo.Money,
                            SourceEmail = currentUser.Email
                        };
                        _session.Transfer(transferData);
                        return RedirectToAction("TransferSucces", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("SourceCard", "Selected card not found or destination user does not exist.");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred during the transfer process.");
            }

            return View(transferInfo);
        }

        public ActionResult TransferSucces()
        {
            return View();
        }

        public ActionResult History()
        {
            var currentUser = System.Web.HttpContext.Current.GetMySessionObject();
            var history = _session.GetHistory(currentUser);
            return View(history);
        }

        public ActionResult Failed()
        {
            return View();
        }

    }
}