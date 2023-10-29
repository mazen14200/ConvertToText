﻿using Bnan.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bnan.Ui.ViewModels.CAS
{
    public class CarPriceVM
    {
        public string CrCasPriceCarBasicNo { get; set; } = null!;
        public string? CrCasPriceCarBasicYear { get; set; }
        public string? CrCasPriceCarBasicType { get; set; }
        public string? CrCasPriceCarBasicLessorCode { get; set; }
        public string? CrCasPriceCarBasicBrandCode { get; set; }
        public string? CrCasPriceCarBasicModelCode { get; set; }
        public string? CrCasPriceCarBasicCategoryCode { get; set; }
        public string? CrCasPriceCarBasicCarYear { get; set; }
        public string? CrCasPriceCarBasicDistributionCode { get; set; }
        public DateTime? CrCasPriceCarBasicDate { get; set; }
        public DateTime? CrCasPriceCarBasicStartDate { get; set; }
        public DateTime? CrCasPriceCarBasicEndDate { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicDailyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicWeeklyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicMonthlyRent { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public int? CrCasPriceCarBasicYearlyRent { get; set; }
        public decimal? CrCasPriceCarBasicWeeklyDay { get; set; }
        public decimal? CrCasPriceCarBasicMonthlyDay { get; set; }
        public decimal? CrCasPriceCarBasicYearlyDay { get; set; }
        public int? CrCasPriceCarBasicNoDailyFreeKm { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicAdditionalKmValue { get; set; }
        public int? CrCasPriceCarBasicFreeAdditionalHours { get; set; }
        public int? CrCasPriceCarBasicHourMax { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public decimal? CrCasPriceCarBasicExtraHourValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public decimal? CrCasPriceCarBasicRentalTaxRate { get; set; }
        public bool? CrCasPriceCarBasicIsAdditionalDriver { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public decimal? CrCasPriceCarBasicAdditionalDriverValue { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public decimal? CrCasCarPriceBasicInFeesTamm { get; set; }
        [Required(ErrorMessage = "requiredFiled")]

        public decimal? CrCasCarPriceBasicOutFeesTamm { get; set; }
        public decimal? CrCasCarPriceBasicInFeesTga { get; set; }
        public decimal? CrCasCarPriceBasicOutFeesTga { get; set; }
        public int? CrCasPriceCarBasicMinAge { get; set; }
        public int? CrCasPriceCarBasicMaxAge { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationAccident { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationFire { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationTheft { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public decimal? CrCasPriceCarBasicCompensationDrowning { get; set; }
        public bool? CrCasPriceCarBasicRequireFinancialCredit { get; set; }
        public int? CrCasPriceCarBasicCancelHour { get; set; }
        public int? CrCasPriceCarBasicAlertHour { get; set; }
        public DateTime? CrCasPriceCarBasicDateAboutToFinish { get; set; }
        public string? CrCasPriceCarBasicStatus { get; set; }
        public string? CrCasPriceCarBasicReasons { get; set; }

        public virtual CrMasSupCarBrand? CrCasPriceCarBasicBrandCodeNavigation { get; set; }
        public virtual CrMasSupCarYear? CrCasPriceCarBasicCarYearNavigation { get; set; }
        public virtual CrMasSupCarCategory? CrCasPriceCarBasicCategoryCodeNavigation { get; set; }
        public virtual CrMasSupCarDistribution? CrCasPriceCarBasicDistributionCodeNavigation { get; set; }
        public virtual CrMasLessorInformation? CrCasPriceCarBasicLessorCodeNavigation { get; set; }
        public virtual CrMasSupCarModel? CrCasPriceCarBasicModelCodeNavigation { get; set; }
        public virtual ICollection<CrCasPriceCarAdditional>? CrCasPriceCarAdditionals { get; set; }
        public virtual ICollection<CrCasPriceCarAdvantage>? CrCasPriceCarAdvantages { get; set; }
        public virtual ICollection<CrCasPriceCarOption>? CrCasPriceCarOptions { get; set; }
    }
}
