
using Microsoft.EntityFrameworkCore;
using PersonOfInterestAPI.Data;
using PersonOfInterestAPI.Models;

namespace PersonOfInterestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            //************************************ PERSONS *******************************************\\

            //Get All Persons
            //GET/persons                                                                                      //Hämta alla personer\\
            app.MapGet("/persons", async (ApplicationDbContext context) =>
            {
                //List All Person in Db
                var persons = await context.Persons.ToListAsync();

                // if No data in Persons
                if(persons == null|| !persons.Any())
                {
                    return Results.NotFound("No persons found");
                }

                //If Data
                return Results.Ok(persons);
            });


            //Create a Person in Db\\
            //POST/persons                                                                                        //skapa ny person\\      
            app.MapPost("/persons", async (Person person, ApplicationDbContext context) =>
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();

                return Results.Created($"/persons/{person.PersonId}", person);
            });




            //*********************************  INTERESTS  *****************************************\\



            //Return all interests\\ 
            //GET/interests                                                                                       //Hämta alla intressen\\
            app.MapGet("/interests", async (ApplicationDbContext context) =>
            {
                //List All Interest in Db
                var interests = await context.Interests.ToListAsync();

                // if No data in Interest
                if (interests == null || !interests.Any())
                {
                    return Results.NotFound("No interests found");
                }

                //If Data
                return Results.Ok(interests);
            });


            //Create a new interest in Db\\
            //POST/interests                                                                                       //skapa nytt intresse (utan att koppla)\\
            app.MapPost("/interests", async (Interest interest, ApplicationDbContext context) =>
            {
                context.Interests.Add(interest);
                await context.SaveChangesAsync();

                return Results.Created($"/interests/{interest.InterestId}", interest);
            });




            //**************************  PERSONINTERESTS  **************************************\\




            //Return all personinterests\\          
            //GET/personinterests                                                                                       //Hämta alla PersonInterests\\
            app.MapGet("/personinterests", async (ApplicationDbContext context) =>
            {
                //List All PersonInterest in Db
                var personInterests = await context.PersonInterests.ToListAsync();

                // if No data in Interest
                if (personInterests == null || !personInterests.Any())
                {
                    return Results.NotFound("No interests found");
                }

                //If Data
                return Results.Ok(personInterests);
            });



            //Create a personinterests in Db\\
            //POST/personinterests                                                                                       //Skapa nytt PersonIntresse  (koppla till nytt intresse)\\
            app.MapPost("/personinterests", async (PersonInterest personInterest, ApplicationDbContext context) =>
            {
                context.PersonInterests.Add(personInterest);
                await context.SaveChangesAsync();

                return Results.Created($"/interests/{personInterest.PersonInterestId}", personInterest);
            });



            //*********** PersonInterests by FkPerson Id ***********\\
            // GET/personinterests/{FkPersonId}                                                                             // hämta alla Personintressen kopplade till en specifik person{FkPersonId}
            app.MapGet("/personinterests/{FkPersonId:int}", async (int FkPersonId, ApplicationDbContext context) =>
            {
                // find all PersonInterests where FkPersonId is connectet, to list
                var personInterest = await context.PersonInterests
                .Where(pfk => pfk.FkPersonId == FkPersonId)
                .ToListAsync();

                // if no personinterests in list
                if (personInterest.Count == 0)
                {
                    return Results.NotFound("No PersonInterests found for Specifies PersonId");
                }

                // return all PersonInterests that's matching and ok
                return Results.Ok(personInterest);
            });




            //**********  LINK  ************\\
            //Return all Links\\
            //GET/links                                                                                                         //Hämta alla Länkar\\
            app.MapGet("/links", async (ApplicationDbContext context) =>
            {
                //List All PersonInterest in Db
                var links = await context.Links.ToListAsync();

                // if No data in Interest
                if (links == null || !links.Any())
                {
                    return Results.NotFound("No interests found");
                }

                //If Data
                return Results.Ok(links);
            });


            //Create a Links in Db connected to a (fkpersoninterest) person and interest\\  
            //POST/links                                                                                                         //Skapa ny länk personer\\
            app.MapPost("/links", async (Link link, ApplicationDbContext context) =>                                             //Koppla direkt på personintresse
                                                                                                                                 //Eller Skapa en person och Intresse direkt och koppla på Personinterest       
            {
                context.Links.Add(link);
                await context.SaveChangesAsync();

                return Results.Created($"/links/{link.LinkId}", link);
            });



            //*********** Links by FkPerson Id ***********\\
            // GET Links by FkPersonId                                                                                           //Hämta alla intressen utefter FkPersonId\\
            app.MapGet("/links/{FkPersonId:int}", async (int FkPersonId, ApplicationDbContext context) =>
            {
                // Find alla Links connected to a specifik FkPersonId, to list
                var links = await context.Links
                .Where(link => link.PersonInterest.FkPersonId == FkPersonId)
                .ToListAsync();

                // No Links in list, return NotFound
                if (links.Count == 0)
                {
                    return Results.NotFound("No links found for the specified person");
                }

                // Else, Return all links connected to specific FkPersonIf
                return Results.Ok(links);
            });



            app.Run();
        }
    }
}
