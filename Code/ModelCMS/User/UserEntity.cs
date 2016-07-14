using System;

namespace ModelCMS.User
{
	public class UserEntity
    {
		#region Properties
		
		public long Id { get; set; }

        public int? ParentId { get; set; }

        public int? UserType { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }
		
		public string Password { get; set; }
		
		public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

        #endregion
    }
}
