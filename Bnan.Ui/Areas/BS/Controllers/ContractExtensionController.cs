﻿using AutoMapper;
using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using Bnan.Inferastructure.Repository;
using Bnan.Ui.Areas.Base.Controllers;
using Bnan.Ui.ViewModels.BS;
using Bnan.Ui.ViewModels.CAS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System.Diagnostics.Contracts;

namespace Bnan.Ui.Areas.BS.Controllers
{
    [Area("BS")]
    public class ContractExtensionController : BaseController
    {
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<ContractExtensionController> _localizer;
        private readonly IAdminstritiveProcedures _adminstritiveProcedures;

        public ContractExtensionController(IStringLocalizer<ContractExtensionController> localizer, IUnitOfWork unitOfWork, UserManager<CrMasUserInformation> userManager, IMapper mapper, IToastNotification toastNotification, IAdminstritiveProcedures adminstritiveProcedures) : base(userManager, unitOfWork, mapper)
        {
            _localizer = localizer;
            _toastNotification = toastNotification;
            _adminstritiveProcedures = adminstritiveProcedures;
        }
        public async Task<IActionResult> Index()
        {
            //To Set Title 
            var titles = await setTitle("501", "5501002", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);

            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicStatus == Status.Active &&
                                                                           x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch &&
                                                                           x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic4.CrCasRenterLessorNavigation" });
            var contractMap = _mapper.Map<List<ContractForExtensionVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode&&
                x.CrCasRenterContractAuthorizationContractNo==contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate=authContract.CrCasRenterContractAuthorizationEndDate;
            }
            bsLayoutVM.ExtensionContracts = contractMap.Where(x=>x.AuthEndDate>DateTime.Now).ToList();
            return View(bsLayoutVM);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetContractBySearch(string search)
        {
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contracts = _unitOfWork.CrCasRenterContractBasic.FindAll(x => x.CrCasRenterContractBasicStatus == Status.Active &&
                                                                           x.CrCasRenterContractBasicBranch == bsLayoutVM.SelectedBranch &&
                                                                           x.CrCasRenterContractBasicLessor == lessorCode, new[] { "CrCasRenterContractBasicCarSerailNoNavigation", "CrCasRenterContractBasic4.CrCasRenterLessorNavigation" });
            var contractMap = _mapper.Map<List<ContractForExtensionVM>>(contracts);
            foreach (var contract in contractMap)
            {
                var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
                if (authContract != null) contract.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            }

            if (!string.IsNullOrEmpty(search))
            {
                
                bsLayoutVM.ExtensionContracts = contractMap.Where(x => x.AuthEndDate > DateTime.Now&&
                                                                                               (x.CrCasRenterContractBasicNo.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic4.CrCasRenterLessorNavigation.CrMasRenterInformationArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateArName.Contains(search) ||
                                                                                                x.CrCasRenterContractBasic4.CrCasRenterLessorNavigation.CrMasRenterInformationEnName.ToLower().Contains(search) ||
                                                                                                x.CrCasRenterContractBasicCarSerailNoNavigation.CrCasCarInformationConcatenateEnName.ToLower().Contains(search))).ToList();
                return PartialView("_ContractExtension", bsLayoutVM);
            }
            bsLayoutVM.ExtensionContracts= contractMap.Where(x => x.AuthEndDate > DateTime.Now ).ToList();
            return PartialView("_ContractExtension", bsLayoutVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            //To Set Title 
            var titles = await setTitle("501", "5501002", "5");
            await ViewData.SetPageTitleAsync(titles[0], titles[1], titles[2], "", "", titles[3]);
            var userLogin = await _userManager.GetUserAsync(User);
            var lessorCode = userLogin.CrMasUserInformationLessor;
            var bsLayoutVM = await GetBranchesAndLayout();
            var contract = await _unitOfWork.CrCasRenterContractBasic.FindAsync(x => x.CrCasRenterContractBasicNo == id,
                                                                                     new[] { "CrCasRenterContractBasic4.CrCasRenterLessorNavigation", 
                                                                                             "CrCasRenterContractBasicCarSerailNoNavigation"});
            var authContract = _unitOfWork.CrCasRenterContractAuthorization.Find(x => x.CrCasRenterContractAuthorizationLessor == lessorCode &&
                x.CrCasRenterContractAuthorizationContractNo == contract.CrCasRenterContractBasicNo);
            var contractMap = _mapper.Map<ContractForExtensionVM>(contract);
            var PaymentMethod = _unitOfWork.CrMasSupAccountPaymentMethod.FindAll(x => x.CrMasSupAccountPaymentMethodStatus == Status.Active).ToList();
            var SalesPoint = _unitOfWork.CrCasAccountSalesPoint.FindAll(x => x.CrCasAccountSalesPointLessor == lessorCode &&
                                                                             x.CrCasAccountSalesPointBrn == bsLayoutVM.SelectedBranch &&
                                                                             x.CrCasAccountSalesPointBankStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointStatus == Status.Active &&
                                                                             x.CrCasAccountSalesPointBranchStatus == Status.Active).ToList();

            contractMap.AuthEndDate = authContract.CrCasRenterContractAuthorizationEndDate;
            contractMap.AuthType = authContract.CrCasRenterContractAuthorizationType;
            bsLayoutVM.ExtensionContract = contractMap;
            bsLayoutVM.SalesPoint = SalesPoint;
            bsLayoutVM.PaymentMethods = PaymentMethod;
            return View(bsLayoutVM);
        }
    }

}
