﻿using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Globalization;
namespace Bnan.Ui.Areas.CAS.Controllers
{
    [Area("CAS")]
    [Authorize(Roles = "CAS")]
    public class TransferFromTenantController : BaseController
    {
        private readonly IUserLoginsService _userLoginsService;
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<TransferFromTenantController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;
        private readonly ITransferToFromRenter _tranferToRenter;


        public TransferFromTenantController(UserManager<CrMasUserInformation> userManager, IUnitOfWork unitOfWork,
             IMapper mapper, IUserService userService, IAccountBank accountBank,
             IUserLoginsService userLoginsService, IToastNotification toastNotification,
             IStringLocalizer<TransferFromTenantController> localizer, IAdminstritiveProcedures adminstritiveProcedures, ITransferToFromRenter tranferToRenter) :
             base(userManager, unitOfWork, mapper)
        {
            _userService = userService;
            _userLoginsService = userLoginsService;
            _toastNotification = toastNotification;
            _localizer = localizer;
            _adminstritiveProcedures = adminstritiveProcedures;
            _tranferToRenter = tranferToRenter;
        }
        public async Task<IActionResult> Index()
        {
            //save Tracing
            var (mainTask, subTask, system, currentUser) = await SetTrace("204", "2204004", "2");

            await _userLoginsService.SaveTracing(currentUser.CrMasUserInformationCode, "عرض بيانات", "View Informations", mainTask.CrMasSysMainTasksCode,
            subTask.CrMasSysSubTasksCode, mainTask.CrMasSysMainTasksArName, subTask.CrMasSysSubTasksArName, mainTask.CrMasSysMainTasksEnName,
            subTask.CrMasSysSubTasksEnName, system.CrMasSysSystemCode, system.CrMasSysSystemArName, system.CrMasSysSystemEnName);


            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var RenterAvaiable = _unitOfWork.CrCasRenterLessor.FindAll(x => x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" }).ToList();
            var RenterAvaiableVM = _mapper.Map<List<RenterLessorVM>>(RenterAvaiable);
            foreach (var item in RenterAvaiableVM)
            {
                item.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == item.CrCasRenterLessorDealingMechanism);
            }
            return View(RenterAvaiableVM);
        }

        [HttpGet]
        public async Task<IActionResult> TransferFrom(string id)
        {
            //sidebar Active
            ViewBag.id = "#sidebarAcount";
            ViewBag.no = "3";
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var titles = await setTitle("204", "2204004", "2");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "تحويل", "Transfer", titles[3]);

            var Renter = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == id && x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" });
            var RenterVM = _mapper.Map<RenterLessorVM>(Renter);

            DateTime year = DateTime.Now;
            var y = year.ToString("yy");
            var Lrecord = _unitOfWork.CrCasSysAdministrativeProcedure.FindAll(x => x.CrCasSysAdministrativeProceduresLessor == lessorCode &&
                x.CrCasSysAdministrativeProceduresCode == "306"
                && x.CrCasSysAdministrativeProceduresSector == "1"
                && x.CrCasSysAdministrativeProceduresYear == y).Max(x => x.CrCasSysAdministrativeProceduresNo.Substring(x.CrCasSysAdministrativeProceduresNo.Length - 6, 6));
            string Serial;
            if (Lrecord != null)
            {
                Int64 val = Int64.Parse(Lrecord) + 1;
                Serial = val.ToString("000000");
            }
            else
            {
                Serial = "000001";
            }
            RenterVM.AdminstritiveNo = y + "-" + "1" + "306" + "-" + lessorCode + "100" + "-" + Serial;
            RenterVM.CrMasSysEvaluation = _unitOfWork.CrMasSysEvaluation.Find(x => x.CrMasSysEvaluationsCode == RenterVM.CrCasRenterLessorDealingMechanism);
            RenterVM.Banks = _unitOfWork.CrMasSupAccountBanks.FindAll(x => x.CrMasSupAccountBankStatus == Status.Active && x.CrMasSupAccountBankCode != "00").ToList();
            RenterVM.AccountBanks = _unitOfWork.CrCasAccountBank.FindAll(x => x.CrCasAccountBankStatus == Status.Active && x.CrCasAccountBankNo != "00" && x.CrCasAccountBankLessor == lessorCode).ToList();
            RenterVM.RenterInformationIban = Renter.CrCasRenterLessorNavigation.CrMasRenterInformationIban;
            RenterVM.BankSelected = Renter.CrCasRenterLessorNavigation.CrMasRenterInformationBank;
            return View(RenterVM);
        }
        [HttpPost]
        public async Task<IActionResult> TransferFrom(RenterLessorVM renterLessorVM)
        {

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;


            var Renter = _unitOfWork.CrCasRenterLessor.Find(x => x.CrCasRenterLessorId == renterLessorVM.CrCasRenterLessorId && x.CrCasRenterLessorCode == lessorCode, new[] {"CrCasRenterContractBasicCrCasRenterContractBasic4s",
                                                                                                                           "CrCasRenterLessorNavigation" });
            var AddAdminstritive = await _tranferToRenter.SaveAdminstritiveTransferRenter(renterLessorVM.AdminstritiveNo, userLogin.CrMasUserInformationCode, "305", "30", lessorCode, Renter.CrCasRenterLessorId,
                                                                                   0, decimal.Parse(renterLessorVM.Amount, CultureInfo.InvariantCulture), renterLessorVM.Reasons);

            var CheckAddReceipt = true;
            CheckAddReceipt = await _tranferToRenter.AddAccountReceiptTransferToRenter(AddAdminstritive.CrCasSysAdministrativeProceduresNo, Renter.CrCasRenterLessorId, userLogin.CrMasUserInformationCode, "302","16", lessorCode,
                                                                                        renterLessorVM.FromBank, renterLessorVM.FromAccountBankSelected,"0", renterLessorVM.Amount,
                                                                                        renterLessorVM.Reasons);
            var CheckUpdateMasRenter = true;
            CheckUpdateMasRenter = await _tranferToRenter.UpdateRenterInformation(Renter.CrCasRenterLessorId, renterLessorVM.RenterInformationIban, renterLessorVM.BankSelected);


            var CheckUpdateRenterLessor = true;
            var AmountMinus = (-decimal.Parse(renterLessorVM.Amount, CultureInfo.InvariantCulture)).ToString();

            CheckUpdateRenterLessor = await _tranferToRenter.UpdateCasRenterLessor(Renter.CrCasRenterLessorId, lessorCode, AmountMinus);


            if (AddAdminstritive != null && CheckAddReceipt && CheckUpdateMasRenter && CheckUpdateRenterLessor)
            {
                if (await _unitOfWork.CompleteAsync() > 1) _toastNotification.AddSuccessToastMessage(_localizer["ToastSave"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
                else _toastNotification.AddErrorToastMessage(_localizer["ToastFailed"], new ToastrOptions { PositionClass = _localizer["toastPostion"] });
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountBankNo(string AccountNo)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var account = await _unitOfWork.CrCasAccountBank.FindAsync(x => x.CrCasAccountBankCode == AccountNo && x.CrCasAccountBankLessor == lessorCode, new[] { "CrCasAccountBankNoNavigation" });
            var result = new
            {
                accountNo = account.CrCasAccountBankCode,
                accountIban = account.CrCasAccountBankIban,
                bankNo = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankCode,
                arBank = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankArName,
                enBank = account.CrCasAccountBankNoNavigation?.CrMasSupAccountBankEnName,
            };
            return Json(result);
        }


    }
}
