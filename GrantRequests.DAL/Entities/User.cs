using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GrantRequests.Common;

namespace GrantRequests.DAL.Entities
{
    public class User
    {
        public User()
        {           
            Requests = new List<Request>();
            TherapeuticAreas = new List<TherapeuticArea>();
            Approvers = new List<User>();
            PointPersonals = new List<User>();
            VoteResults = new List<VoteResult>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Name { get; set; }
        [Required]
        [StringLength(512)]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<TherapeuticArea> TherapeuticAreas { get; set; }

        public virtual ICollection<User> Approvers { get; set; }

        public virtual ICollection<User> PointPersonals { get; set; }

        public virtual ICollection<VoteResult> VoteResults { get; set; }

    }
}
