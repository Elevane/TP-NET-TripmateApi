
using TripmateApi.Domain.Entities;
using System;

namespace TripmateApi.Entities.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Step>? Voyages { get; set; }
    }
}
