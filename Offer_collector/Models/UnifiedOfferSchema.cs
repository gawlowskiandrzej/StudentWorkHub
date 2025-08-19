using System;
using System.Collections.Generic;

public class Company
{
    public string name { get; set; }
    public object logoUrl { get; set; }
}

public class Coordinates
{
    public object latitude { get; set; }
    public object longitude { get; set; }
}

public class Dates
{
    public string published { get; set; }
    public object expires { get; set; }
}

public class Employment
{
    public List<object> types { get; set; }
    public List<object> schedules { get; set; }
}

public class Location
{
    public object buildingNumber { get; set; }
    public object street { get; set; }
    public object city { get; set; }
    public object postalCode { get; set; }
    public Coordinates coordinates { get; set; }
    public object isRemote { get; set; }
    public object isHybrid { get; set; }
}

public class Requirements
{
    public object skills { get; set; }
    public object experienceLevel { get; set; }
    public object experienceYears { get; set; }
    public object education { get; set; }
    public object languages { get; set; }
}

public class UnifiedOfferSchema
{
    public object id { get; set; }
    public string source { get; set; }
    public string url { get; set; }
    public string jobTitle { get; set; }
    public Company company { get; set; }
    public object description { get; set; }
    public Salary salary { get; set; }
    public Location location { get; set; }
    public Requirements requirements { get; set; }
    public Employment employment { get; set; }
    public Dates dates { get; set; }
    public object benefits { get; set; }
    public bool isUrgent { get; set; }
    public bool isForUkrainians { get; set; }
}

public class Salary
{
    public object from { get; set; }
    public object to { get; set; }
    public object currency { get; set; }
    public object period { get; set; }
    public object type { get; set; }
}
