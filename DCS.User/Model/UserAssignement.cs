using DCS.CoreLib;
using DCS.CoreLib.BaseClass;

namespace DCS.User
{
    /// <summary>
    /// Represents an assignment of a user to a group, organization, and role, including membership and audit
    /// information.
    /// </summary>
    /// <remarks>Use this class to model the association between a user and their assigned group,
    /// organization, and role within a system. The class also tracks membership duration and audit timestamps for
    /// creation and updates. All identifiers are represented as GUIDs to ensure uniqueness across the system.</remarks>
    public class UserAssignement : ModelBase, IEntity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssignement"/> class.
        /// </summary>
        public UserAssignement() { }

        /// <summary>
        /// Gets or sets the unique identifier for the user to assign.
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the group to assign to.
        /// </summary>
        public Guid GroupGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the organization to assign to.
        /// </summary>
        public Guid OrganisationGuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the role to assign to.
        /// </summary>
        public Guid RoleGuid { get; set; }

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
