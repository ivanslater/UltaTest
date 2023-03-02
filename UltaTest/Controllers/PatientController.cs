using AutoMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UltaTest.Data.Domains;
using UltaTest.Helpers;
using UltaTest.Models;
using UltaTest.Services;

namespace UltaTest.Controllers
{
    public class PatientController : Controller
    {
        public readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string filter = "")
        {
            try
            {
                var patients = _patientService.ListAll(filter).ToList();

                var model = new PagedList<PatientModel>
                {
                    PageSize = 999,
                    Content = _mapper.Map<List<PatientModel>>(patients)
                };

                return PartialView("_List", model);
            }
            catch (Exception ex)
            {
                NotificationsHelper.AddNotification(new Notification { Message = ex.Message, NotificationType = NotificationType.Error, Title = "Error" });
            }

            return PartialView("_List", new PagedList<PatientModel> { PageSize = 1, Content = new List<PatientModel>() });
        }

        public PartialViewResult AddOrEdit(int id = 0)
        {
            var model = new PatientModel() { DateOfBirth = new DateTime(1980, 1, 1) };

            if (id > 0)
            {
                try
                {
                    var patient = _patientService.GetById(id);
                    model = _mapper.Map<PatientModel>(patient);
                }
                catch (Exception ex)
                {
                    NotificationsHelper.AddNotification(new Notification { Message = ex.Message, NotificationType = NotificationType.Error, Title = "Error" });
                }
            }

            return PartialView("_AddOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(PatientModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var patient = _mapper.Map<Patient>(model);

                    if (patient.Id > 0)
                    {
                        _patientService.Update(patient);
                        NotificationsHelper.AddNotification(new Notification { Message = "Patient profile was updated!", NotificationType = NotificationType.Success });
                    }
                    else
                    {
                        _patientService.Add(patient);
                        NotificationsHelper.AddNotification(new Notification { Message = "New patient was added!", NotificationType = NotificationType.Success });
                    }
                }
                catch (Exception ex)
                {
                    NotificationsHelper.AddNotification(new Notification { Message = ex.Message, NotificationType = NotificationType.Error, Title = "Error" });
                }
            }
            else
                NotificationsHelper.AddNotification(new Notification { Message = String.Join("<br>", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToArray()), NotificationType = NotificationType.Error, Title = "Error" });

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _patientService.Delete(id);
                NotificationsHelper.AddNotification(new Notification { Message = "Patient was deleted!", NotificationType = NotificationType.Success });
            }
            catch (Exception ex)
            {
                NotificationsHelper.AddNotification(new Notification { Message = ex.Message, NotificationType = NotificationType.Error, Title = "Error" });
            }

            return RedirectToAction("Index");
        }
    }
}