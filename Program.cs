using System;
using System.Collections.Generic;
using System.Reflection;
using static Program;

/// <summary>
///  Represnts a program that validates the skills of a player
/// </summary>
///<include file='' path='[@name=""]'/> 

internal class Program
{
    /// <summary>
    /// Represents the entry point of the program
    /// </summary>
    /// <param name="args"> The command-line arguments </param>
    private static void Main(string[] args)
    {
        /// <summary>
        /// 

        List<player> players = new List<player>()
    {
        new player
        {
            name = "messi",
            id = 10,
            shooting = 99,
            passing = 99,
            dribbling = 99,
            defending = 99,
            speed = 99,
            physical = 99,
            overall = 99
        },
        new player
        {
            name = "ronaldo",
            id = 7,
            shooting = 99,
            passing = 99,
            dribbling = 99,
            defending = 99,
            speed = 99,
            physical = 99,
            overall = 99    

        },
        new player
        {
            name = "neymar",
            id = 11,
            shooting = 99,
            passing = 99,
            dribbling = 99,
            defending = 99,
            speed = 99,
            physical = 99,
            overall = 99

        },
        new player
        {
            name = "mbappe",
            id = 7,
            shooting = 89,
            passing = 49,
            dribbling = 79,
            defending = 59,
            speed = 100,
            physical = 92,
            overall = 97

        },
        new player
        {
            name = "hazard",
            id = 17,
            shooting = 92,
            passing = 94,
            dribbling = 92,
            defending = 01,
            speed = 91,
            physical = 90,
            overall = 93

        },
        new player
        {
            name = "kane",
            id = 71,
            shooting = 93,
            passing = 909,
            dribbling = 56,
            defending = 59,
            speed = 79,
            physical = 79,
            overall = 91

        },
        new player
        {
            name = "aguero",
            id = 9,
            shooting = 90,
            passing = 999,
            dribbling = 87,
            defending = 57,
            speed = 79,
            physical = 69,
            overall = 85

        }, 
   
    };
        

        var errors =new List<Error>();
        foreach(var player in players)
        {
            var properties = player.GetType().GetProperties();
            foreach(var property in properties)
            {
                var SkillAtributes = property.GetCustomAttributes<SkillAtribute>(); 
                if(SkillAtributes is not null)
                {
                    var value = property.GetValue(player);
                    foreach(var SkillAtribute in SkillAtributes)
                    {
                        if(!SkillAtribute.isValid(value))
                        {
                            errors.Add(new Error($"{SkillAtribute.name} is not valid", property.Name,player.name));
                        }
                    }   
                }

            }
        }
        if (errors.Count > 0)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

        }
        else
        {
            Console.WriteLine("All players are valid");
        }

    }


    /// <include file='CAreflection.xml' path='docs/members[@name="player"]/player/*'/>


    public class player
    {
        /// <summary>
        /// Gets or sets the name of the player
        /// </summary>
        public string name { get;  set; }

       /// <summary>
       /// gets or sets the id of the player
       /// </summary>
        public int id { get;  set; }

        /// <summary>
        /// gets or sets the shooting skill of the player
        /// </summary>
        [SkillAtribute(nameof(shooting), 1, 99)]
        public int shooting { get; set; }

        /// <summary>
        /// gets or sets the passing skill of the player
        /// </summary>
        [SkillAtribute(nameof(passing), 1, 99)]
        public int passing { get; set; }

        /// <summary>
        /// gets or sets the dribbling skill of the player
        /// </summary>
        [SkillAtribute(nameof(dribbling), 1, 100)]
        public int dribbling { get; set; }

        /// <summary>
        /// gets or sets the defending skill of the player
        /// </summary>
        [SkillAtribute(nameof(defending), 1, 100)]
        public int defending { get; set;}

        /// <summary>
        /// gets or sets the speed skill of the player
        /// </summary>
        [SkillAtribute(nameof(physical), 1, 99)]
        public int speed { get; set; }

        /// <summary>
        /// gets or sets the physical skill of the player
        /// </summary>
        [SkillAtribute(nameof(physical), 1, 99)]
        public int physical { get; set; }

        /// <summary>
        /// gets or sets the overall skill of the player
        /// </summary>
        [SkillAtribute(nameof(overall), 0, 99)]
        public int overall { get; set; }

    }

    /// <summary>
    /// Represents a custome  skill attribute
    /// </summary>
    /// <remarks>
    /// This attribute is used to specify the name of the skill and the min and max values
    /// </remarks>
    /// <example>
    /// This example demonstrates the usage of the SkillAttribute class:
    /// <code>
    ///[SkillAttribute(nameof(shooting), 1, 99)]
    ///public int shooting { get; set; }
    ///  </code>
    ///</example>
    ///<seealso cref="player"/>
    ///<seealso cref="Error"/>
    ///<seealso cref="List{T}"/>
    ///<seealso cref="Attribute"/>
    ///<seealso cref="AttributeUsageAttribute"/>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false,Inherited =false)]
    public class SkillAtribute : Attribute
    {
        /// <summary>
        /// Gets the name of the skill
        /// </summary>
        public string name { get; private set; }
        /// <summary>
        /// gets the min value of the skill
        /// </summary>
        public int min { get; private set; }
        /// <summary>
        /// gets the max value of the skill
        /// </summary>
        public int max { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillAtribute"/> class
        /// </summary>
        /// <param name="name">The name of the skill </param>
        /// <param name="min">The min value of the skill </param>
        /// <param name="max">the max value of the skill</param>
        public SkillAtribute(string name, int min, int max)
        {
            this.name = name;
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Determines whether the specified object is valid
        /// </summary>
        /// <param name="obj">The object value to be validated</param>
        /// <returns>True if the value is within the specified range, otherwise false .</returns>
        public bool isValid(object obj)
        {
            var value = (int)obj;
            return value >= min && value <= max;
        }   

       
    }

    /// <summary>
    /// Represents an error that occured during the validation of a player
    /// </summary>
    /// <remarks>
    /// This class is used to encapsulate and represent errors that occur when validating the skill attributes of a player. It contains information about the specific error message, the field associated with the error, and the name of the player whose data is invalid.
    /// </remarks>
    /// <example>   
    /// this example demonstrate the usage of error class
    /// <code>
    /// Error error = new Error("shooting is not valid", "shooting", "messi");
    /// </code>
    /// </example>
    /// <seealso cref="player"/>
    /// <seealso cref="SkillAtribute"/>
    /// <seealso cref="List{T}"/>
    /// <Seealso cref="ToString"/>
    /// <seealso cref="string"/>
    public class Error
    {
        
        private string _message;
        private string _field;
        private string _playerName;

       
        /// <param name="message"> The error message </param>
        /// <param name="field">The field that conrain the error</param>
        /// <param name="playerName"> The player name who's data is invalid</param>
       public Error(string message, string field,string playerName)
       {
            this._message = message;
            this._field = field;
            this._playerName = playerName;

       }
        /// <summary>
        /// Returns a string representation of the error message, field, and associated player name.
        /// </summary>
        /// <returns>A formatted string with the error details.</returns>
        public override string ToString()
        {
            return $"{{\"PlayerName\": \"{_playerName}\", \"{_field}\" : \"{_message}\"}}";
        }
    }
}
