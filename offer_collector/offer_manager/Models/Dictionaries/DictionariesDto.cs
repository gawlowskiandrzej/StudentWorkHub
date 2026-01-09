namespace offer_manager.Models.Dictionaries
{

    /// <summary>
    /// DTO dla pojedynczego elementu słownika z ID i nazwą właściwości
    /// </summary>
    public class DictionaryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DictionariesDto
    {
        public List<string> EmploymentType { get; set; } = new List<string>();
        public List<string> EmploymentSchedules { get; set; } = new List<string>();
        public List<string> SalaryPeriods { get; set; } = new List<string>();
    }

    /// <summary>
    /// DTO dla wszystkich słowników używanych podczas tworzenia profilu użytkownika 
    /// </summary>
    public class AllDictionariesDto
    {
        public List<DictionaryItemDto> LeadingCategories { get; set; } = new List<DictionaryItemDto>();
        public List<DictionaryItemDto> EmploymentTypes { get; set; } = new List<DictionaryItemDto>();
        public List<DictionaryItemDto> Languages { get; set; } = new List<DictionaryItemDto>();
        public List<DictionaryItemDto> LanguageLevels { get; set; } = new List<DictionaryItemDto>();
    }
}
