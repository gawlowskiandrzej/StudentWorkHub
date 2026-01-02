namespace Offer_collector.Models
{
    public enum EmploymentType
    {
        EmploymentContract = 1,     
        MandateContract = 2,         
        SpecificTaskContract = 3,    
        B2BContract = 4,             
        PaidInternship = 5,          
        UnpaidInternship = 6,        
        StudentPractice = 7,         
        Volunteering = 8
    }
    public enum SalaryPeriod
    {
        None = 0,
    }
    public enum WorkTimeType
    {
        FullTimeStandardHours = 1,     
        FullTimeShiftWork = 2,          
        FullTimeNightWork = 3,         
        FullTimeWeekendWork = 4,       

        PartTimeStandardHours = 5,      
        PartTimeShiftWork = 6,         
        PartTimeNightWork = 7,          
        PartTimeWeekendWork = 8,       

        FlexibleWorkingHours = 9,      
        TaskBasedSystem = 10       
    }
	public enum OfferSitesTypes
	{
		Pracujpl = 1,
		Justjoinit = 2,
		Olxpraca = 3,
		Aplikujpl = 4
	}
	public enum ClientType
    {
        httpClient,
        headlessBrowser
    }
    public enum BathStatus
    {
        error,
        completed,
        queued,
        running,
        pending
    }
}
