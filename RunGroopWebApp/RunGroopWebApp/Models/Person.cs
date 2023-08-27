﻿namespace RunGroopWebApp.Models
{
    public class Person
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string RG { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
