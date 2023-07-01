using AEBITRestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AEBITRestfulAPI.Models;
[ApiController]
[Route("[controller]")]
public class AuthModels
{

    public class RegistrationRequest
    {
        public string email { get; set; }
        //[MinLength(6)]
        public string password { get; set; }
    }
    [Table("users")]
    public class User
    {
        [Column("code")]
        [Key]
        public string? code { get; set; }
        [Column("email")]
        public string? email { get; set; }
        [Column("password")]
        public string? password { get; set; }
        
    }

    [Table ("posts")]
    public class Post
    {
        [Column ("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("created_at")]
        public DateTime created_at { get; set;}
        [Column("title")]
        public string title { get; set; }
        [Column("text")]
        public string text { get; set; }
    }

    public class AuthenticationRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }

    public class AuthenticationResponse
    {
        public string? email { get; set; }
        public string? token { get; set; }
    }

    public class RequestPost
    {
        public string title { get; set; }
        public string text { get; set; }
    }

    public class ExceptionMessage
    {
        public string? message { get; set; }
    }
    public class SearchText
    {
        public string text { get; set; }
    }
}