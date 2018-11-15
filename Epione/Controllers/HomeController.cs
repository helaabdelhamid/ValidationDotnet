using AutoMapper;
using Chat.Web.Helpers;
using Chat.Web.Hubs;
using Chat.Web.Models.ViewModels;
using DATA;
using Domaine;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Epione.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chat()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View();
        }
        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var file = Request.Files[0];

                    // Some basic checks...
                    if (file != null && !FileValidator.ValidSize(file.ContentLength))
                        return Json("File size too big. Maximum File Size: 500KB");
                    else if (FileValidator.ValidType(file.ContentType))
                        return Json("This file extension is not allowed!");
                    else
                    {
                        // Save file to Disk
                        var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Content/uploads/"), fileName);
                        file.SaveAs(filePath);

                        string htmlImage = string.Format(
                            "<a href=\"/Content/uploads/{0}\" target=\"_blank\">" +
                            "<img src=\"/Content/uploads/{0}\" class=\"post-image\">" +
                            "</a>", fileName);

                        using (var db = new EpioneContext())
                        {
                            // Get sender & chat room
                            var senderViewModel = ChatHub._Connections.Where(u => u.Username == User.Identity.Name).FirstOrDefault();
                            var sender = db.Users.Where(u => u.UserName == senderViewModel.Username).FirstOrDefault();
                            var room = db.Rooms.Where(r => r.Name == senderViewModel.CurrentRoom).FirstOrDefault();

                            // Build message
                            Message msg = new Message()
                            {
                                Content = Regex.Replace(htmlImage, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                                Timestamp = DateTime.Now.Ticks.ToString(),
                                FromUser = sender,
                                ToRoom = room
                            };

                            db.Messages.Add(msg);
                            db.SaveChanges();

                            // Send image-message to group
                            var messageViewModel = Mapper.Map<Message, MessageViewModel>(msg);
                            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                            hub.Clients.Group(senderViewModel.CurrentRoom).newMessage(messageViewModel);
                        }

                        return Json("Success");
                    }

                }
                catch (Exception ex)
                { return Json("Error while uploading" + ex.Message); }
            }

            return Json("No files selected");

        } // Upload


    }
}