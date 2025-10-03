using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.User
{
    /// <summary>
    /// Represents the assignment of a user to a group, organisation, and/or role.
    /// </summary>
    public class UserAssignement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignement"/> class.
        /// </summary>
        public UserAssignement() { }

        /// <summary>
        /// Gets or sets the unique identifier for the assignement.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user to assign.
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the group to assign to.
        /// </summary>
        public Guid? GroupGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the organization to assign to.
        /// </summary>
        public Guid? OrganisationGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the role to assign to.
        /// </summary>
        public Guid? RoleGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the assignement is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the assignement was created.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the most recent manipulation or update of the assignement.
        /// </summary>
        public DateTime? LastManipulation { get; set; }

        /// <summary>
        /// Gets or sets the start date of the membership.
        /// </summary>
        public DateTime? MembershipStart { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the membership ends.
        /// </summary>
        public DateTime? MembershipEnd { get; set; }
    }
}
