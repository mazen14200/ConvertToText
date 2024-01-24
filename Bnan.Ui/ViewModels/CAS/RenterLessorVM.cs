﻿using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class RenterLessorVM
    {
        public string CrCasRenterLessorId { get; set; } = null!;
        public string CrCasRenterLessorCode { get; set; } = null!;
        public string? CrCasRenterLessorSector { get; set; }
        public int? CrCasRenterLessorCopyId { get; set; }
        public string? CrCasRenterLessorIdtrype { get; set; }
        public string? CrCasRenterLessorMembership { get; set; }
        public DateTime? CrCasRenterLessorDateFirstInteraction { get; set; }
        public DateTime? CrCasRenterLessorDateLastContractual { get; set; }
        public int? CrCasRenterLessorContractCount { get; set; }
        public int? CrCasRenterLessorContractExtension { get; set; }
        public int? CrCasRenterLessorContractDays { get; set; }
        public int? CrCasRenterLessorContractKm { get; set; }
        public decimal? CrCasRenterLessorContractTradedAmount { get; set; }
        public decimal? CrCasRenterLessorBalance { get; set; }
        public int? CrCasRenterLessorEvaluationNumber { get; set; }
        public int? CrCasRenterLessorEvaluationTotal { get; set; }
        public decimal? CrCasRenterLessorEvaluationValue { get; set; }
        public string? CrCasRenterLessorStatisticsNationalities { get; set; }
        public string? CrCasRenterLessorStatisticsGender { get; set; }
        public string? CrCasRenterLessorStatisticsJobs { get; set; }
        public string? CrCasRenterLessorStatisticsRegions { get; set; }
        public string? CrCasRenterLessorStatisticsCity { get; set; }
        public string? CrCasRenterLessorStatisticsAge { get; set; }
        public string? CrCasRenterLessorStatisticsTraded { get; set; }
        public string? CrCasRenterLessorDealingMechanism { get; set; }
        public string? CrCasRenterLessorStatus { get; set; }
        public string? CrCasRenterLessorReasons { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? BankSelected { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? TransferDate { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? FromAccountBankSelected { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? Amount { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? RenterInformationIban { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? FromBank { get; set; }
        public string? AdminstritiveNo { get; set; }
        public string? Reasons { get; set; }
        public virtual CrMasLessorInformation CrCasRenterLessorCodeNavigation { get; set; } = null!;
        public virtual CrMasSupRenterIdtype? CrCasRenterLessorIdtrypeNavigation { get; set; }
        public virtual CrMasSupRenterMembership? CrCasRenterLessorMembershipNavigation { get; set; }
        public virtual CrMasRenterInformation CrCasRenterLessorNavigation { get; set; } = null!;
        public virtual CrMasSupRenterSector? CrCasRenterLessorSectorNavigation { get; set; }
        public virtual CrMasSupPostCity? CrCasRenterLessorStatisticsCityNavigation { get; set; }
        public virtual CrMasSupRenterGender? CrCasRenterLessorStatisticsGenderNavigation { get; set; }
        public virtual CrMasSupRenterProfession? CrCasRenterLessorStatisticsJobsNavigation { get; set; }
        public virtual CrMasSupRenterNationality? CrCasRenterLessorStatisticsNationalitiesNavigation { get; set; }
        public virtual CrMasSupPostRegion? CrCasRenterLessorStatisticsRegionsNavigation { get; set; }
        public virtual ICollection<CrCasRenterContractBasic>? CrCasRenterContractBasicCrCasRenterContractBasic4s { get; set; }
        public virtual ICollection<CrCasRenterContractStatistic>? CrCasRenterContractStatistics { get; set; }
        public virtual CrMasSysEvaluation? CrMasSysEvaluation { get; set; }
        public virtual List<CrMasSupAccountBank>? Banks { get; set; }
        public virtual List<CrCasAccountBank>? AccountBanks { get; set; }
    }
}
