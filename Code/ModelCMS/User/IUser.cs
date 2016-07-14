using System;
using System.Collections.Generic;

namespace ModelCMS.User
{
	public interface IUser
    {
		long Insert(UserEntity obj);		
        bool Update(UserEntity obj);
	    bool UpdateAvatar(UserEntity obj);
        bool Delete(long id, long userId, int userType);
		List<UserEntity> ListAll();
        List<UserEntity> ListAllPaging(UserEntity userInfo, int pageIndex, int pageSize, string sortColumn, string sortDesc, ref int totalRow);
		UserEntity ViewDetail(string id);
	    bool CheckUsernameExist(string username);
	    bool Login(string userName, string password, ref UserEntity obj);
        bool ChangePassword(long id, string oldPassword,string newPassword);
	    bool CheckResetPassword(string oldPass, string email);
	    bool ResetPassword(string email, string password);
	    UserEntity GetUserByEmail(string email);
	}
}
