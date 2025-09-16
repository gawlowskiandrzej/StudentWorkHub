namespace Offer_collector.Models.AI
{
    public class AiPromptParameters
    {
        public string ExampleRequirementsStructure = @"
                ```json\n
           {\n  
            \""skills\"": 
                [\n    
                    { \""name\"": \""Apache Spark (PySpark)\"", \""years\"": 3 },\n
                    { \""name\"": \""Hadoop\"", \""years\"": 2 },\n    
                    { \""name\"": \""Big Data ecosystem\"", \""years\"": 1 },\n    
                    { \""name\"": \""Python\"", \""years\"": null },\n    
                    { \""name\"": \""SQL\"", \""years\"": null },\n    
                    { \""name\"": \""Hive\"", \""years\"": null },\n    
                    { \""name\"": \""HDFS\"", \""years\"": null },\n    
                    { \""name\"": \""Kafka\"", \""years\"": null },\n    
                    { \""name\"": \""Data integration\"", \""years\"": null }\n  
                ],\n  
            \""education\"": 
                 [   
                        \""szkoła średnia\"", 
                        \""studia informatyczne\""
                 ],\n  
            \""languages\"": 
                 [\n    
                    { 
                        \""name\"": \""angielski\"", 
                        \""level\"": \""B2\"" 
                    }\n  
                 ],\n  
            \""benefits\"": 
                 [\n    
                        \""Long-term B2B contract\"",\n    
                        \""Opportunity for growth in a large organization\"",\n    
                        \""Direct contact with team manager\"",\n    
                        \""Work in a mature and global IT structure\"",\n    
                        \""Agile methodologies respected\"",\n    
                        \""Subsidy for MultiSport card\"",\n    
                        \""Subsidy for insurance\"",\n    
                        \""Flexible start hours: 7:30 - 9:30\"",\n    
                        \""Efficient recruitment process (video call)\"",\n    
                        \""Max. 2 online meetings (team + manager)\""\n 
                 ]\n
            }\n```
            \n
        ";

        public string ExampleDescriptionStructure = @"
            Min. 3 lata doświadczenia w programowaniu w Apache Spark, szczególnie PySpark\n
            \nPraktyczna znajomość Hadoop w kontekście tworzenia rozwiązań do przetwarzania dużych zbiorów danych (batch i/lub streaming)\n
            \nUmiejętność projektowania i implementacji procesów przetwarzania danych w ekosystemie Big Data\n\nDoświadczenie w pracy z Python i SQL\n
            \nZnajomość narzędzi wspierających przetwarzanie danych (np. Hive, HDFS, Kafka)\n
            \nDoświadczenie w integracji danych z różnych źródeł\n\nDługofalowe stabilne zatrudnienie w oparciu o kontrakt B2B\n
            \nSzansa na rozwój w dużej strukturze, bezpośredni kontakt z Managerem zespołu\n\nPraca w firmie, w której IT jest dojrzałe i globalne, a metodyki zwinne są respektowane\n
            \nDopłata do karty MultiSport oraz ubezpieczenia\n
            \nGodziny startu pracy 7:30 - 9:30\n
            \nSprawny proces rekrutacyjny (wideokonferencja), maksymalnie 2 spotkania online (pierwsze z kimś z zespołu, drugie z Managerem)\n
        ";

        public string ExampleTaskStructure = @"Now extract structured data from the following text (in Polish) and return only the JSON object according to the format above.\n";
    }
    public enum PromptType
    {
        FromListOfSkills,
        FromDescription
    }
}
