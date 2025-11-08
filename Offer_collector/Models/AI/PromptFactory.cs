namespace Offer_collector.Models.AI
{
    //public static class PromptFactory
    //{
    //    public static AiPromptParameters GetPromptParameters(PromptType variant)
    //    {
    //        return variant switch
    //        {
    //            PromptType.FromDescription => new AiPromptParameters
    //            {
    //                ExampleRequirementsStructure = @"
    //                        ```json\n
    //                   {\n  
    //                    \""skills\"": 
    //                        [\n    
    //                            { \""name\"": \""Apache Spark (PySpark)\"", \""experienceMonths\"": 36, \""experienceLevel\"": \""experienced\"" },\n
    //                            { \""name\"": \""Hadoop\"", \""experienceMonths\"": null, \""experienceLevel\"": \""veryGood\"" },\n    
    //                            { \""name\"": \""Big Data ecosystem\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""Python\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""SQL\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""Hive\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""HDFS\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""Kafka\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""Data integration\"", \""experienceMonths\"": null, \""experienceLevel\"": \""notRecognised\"" }\n  
    //                        ],\n  
    //                    \""education\"": 
    //                         [   
    //                                \""szkoła średnia\"", 
    //                                \""studia informatyczne\""
    //                         ],\n  
    //                    \""languages\"": 
    //                         [\n    
    //                            { 
    //                                \""name\"": \""angielski\"", 
    //                                \""level\"": \""B2\"" 
    //                            }\n  
    //                         ],\n  
    //                    \""benefits\"": 
    //                         [\n    
    //                                \""Long-term B2B contract\"",\n    
    //                                \""Opportunity for growth in a large organization\"",\n    
    //                                \""Direct contact with team manager\"",\n    
    //                                \""Work in a mature and global IT structure\"",\n    
    //                                \""Agile methodologies respected\"",\n    
    //                                \""Subsidy for MultiSport card\"",\n    
    //                                \""Subsidy for insurance\"",\n    
    //                                \""Flexible start hours: 7:30 - 9:30\"",\n    
    //                                \""Efficient recruitment process (video call)\"",\n    
    //                                \""Max. 2 online meetings (team + manager)\""\n 
    //                         ]\n
    //                    }\n```
    //                    \n
    //                ",

    //                ExampleDescriptionStructure = @"
    //                    Min. 3 lata doświadczenia w programowaniu w Apache Spark, szczególnie PySpark\n
    //                    \nBardzo dobra znajomość znajomość Hadoop w kontekście tworzenia rozwiązań do przetwarzania dużych zbiorów danych (batch i/lub streaming)\n
    //                    \nPraktyczna znajomość projektowania i implementacji procesów przetwarzania danych w ekosystemie Big Data\n
    //                    \nDoświadczenie w pracy z Python i SQL\n
    //                    \nPodstawowa znajomość narzędzi wspierających przetwarzanie danych (np. Hive, HDFS, Kafka)\n
    //                    \nDoświadczenie w integracji danych z różnych źródeł\n
    //                    \nDługofalowe stabilne zatrudnienie w oparciu o kontrakt B2B\n
    //                    \nSzansa na rozwój w dużej strukturze, bezpośredni kontakt z Managerem zespołu\n
    //                    \nPraca w firmie, w której IT jest dojrzałe i globalne, a metodyki zwinne są respektowane\n
    //                    \nDopłata do karty MultiSport oraz ubezpieczenia\n
    //                    \nGodziny startu pracy 7:30 - 9:30\n
    //                    \nSprawny proces rekrutacyjny (wideokonferencja), maksymalnie 2 spotkania online (pierwsze z kimś z zespołu, drugie z Managerem)\n
    //                ",
    //                           ExampleTaskStructure = @"Now extract structured data from the following text (in Polish) and return only the JSON object according to the format above.Take care of experienced level recognised one of [\""begginer\"", \""good\"", \""veryGood\"" ,\""experienced\"", \\""notRecognised\\""]\n",
    //                },
    //                        PromptType.FromListOfSkills => new AiPromptParameters
    //                        {
    //                            ExampleRequirementsStructure = @"
    //                        ```json\n
    //                   {\n  
    //                    \""skills\"": 
    //                        [\n    
    //                            { \""name\"": \""Apache Spark (PySpark)\"", \""experienceMonths\"": 36, \""experienceLevel\"": \""experienced\"" },\n
    //                            { \""name\"": \""Hadoop\"", \""experienceMonths\"": null, \""experienceLevel\"": \""veryGood\"" },\n    
    //                            { \""name\"": \""Big Data ecosystem\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""Python\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""SQL\"", \""experienceMonths\"": null, \""experienceLevel\"": \""experienced\"" },\n    
    //                            { \""name\"": \""Hive\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""HDFS\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""Kafka\"", \""experienceMonths\"": null, \""experienceLevel\"": \""beginner\"" },\n    
    //                            { \""name\"": \""Data integration\"", \""experienceMonths\"": null, \""experienceLevel\"": \""notRecognised\"" }\n  
    //                        ],\n  
    //                    \""education\"": 
    //                         [   
    //                                \""szkoła średnia\"", 
    //                                \""studia informatyczne\""
    //                         ],\n  
    //                    \""languages\"": 
    //                         [\n    
    //                            { 
    //                                \""name\"": \""angielski\"", 
    //                                \""level\"": \""B2\"" 
    //                            }\n  
    //                         ],\n  
    //                    \""benefits\"": 
    //                         [\n    
    //                                \""Long-term B2B contract\"",\n    
    //                                \""Opportunity for growth in a large organization\"",\n    
    //                                \""Direct contact with team manager\"",\n    
    //                                \""Work in a mature and global IT structure\"",\n    
    //                                \""Agile methodologies respected\"",\n    
    //                                \""Subsidy for MultiSport card\"",\n    
    //                                \""Subsidy for insurance\"",\n    
    //                                \""Flexible start hours: 7:30 - 9:30\"",\n    
    //                                \""Efficient recruitment process (video call)\"",\n    
    //                                \""Max. 2 online meetings (team + manager)\""\n 
    //                         ]\n
    //                    }\n```
    //                    \n
    //                ",

    //                            ExampleDescriptionStructure = @"
    //                    Min. 3 lata doświadczenia w programowaniu w Apache Spark, szczególnie PySpark\n
    //                    \nBardzo dobra znajomość znajomość Hadoop w kontekście tworzenia rozwiązań do przetwarzania dużych zbiorów danych (batch i/lub streaming)\n
    //                    \nPraktyczna znajomość projektowania i implementacji procesów przetwarzania danych w ekosystemie Big Data\n
    //                    \nDoświadczenie w pracy z Python i SQL\n
    //                    \nPodstawowa znajomość narzędzi wspierających przetwarzanie danych (np. Hive, HDFS, Kafka)\n
    //                    \nDoświadczenie w integracji danych z różnych źródeł\n
    //                    \nDługofalowe stabilne zatrudnienie w oparciu o kontrakt B2B\n
    //                    \nSzansa na rozwój w dużej strukturze, bezpośredni kontakt z Managerem zespołu\n
    //                    \nPraca w firmie, w której IT jest dojrzałe i globalne, a metodyki zwinne są respektowane\n
    //                    \nDopłata do karty MultiSport oraz ubezpieczenia\n
    //                    \nGodziny startu pracy 7:30 - 9:30\n
    //                    \nSprawny proces rekrutacyjny (wideokonferencja), maksymalnie 2 spotkania online (pierwsze z kimś z zespołu, drugie z Managerem)\n
    //                ",
    //                            ExampleTaskStructure = @"Now extract structured data from the following text (in Polish) and return only the JSON object according to the format above.Take care of experienced level recognised one of [\""begginer\"", \""good\"", \""veryGood\"" ,\""experienced\"", \""notRecognised\""]\n",
    //                        }
    //        };
    //    }
    //}
}
