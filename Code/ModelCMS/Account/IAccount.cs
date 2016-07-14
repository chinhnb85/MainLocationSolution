using System;
using System.Collections.Generic;

namespace ModelCMS.Account
{
	public interface IAccount
	{
		long Insert(AccountEntity account);		
        bool Update(AccountEntity account);
	    bool UpdateAvatar(AccountEntity account);
        bool Delete(long id, long adminId, int adminType);
		List<AccountEntity> ListAll();
        List<AccountEntity> ListAllPaging(AccountEntity userInfo, int pageIndex, int pageSize, string sortColumn, string sortDesc, ref int totalRow);
		AccountEntity ViewDetail(string id);
	    bool CheckUsernameExist(string username);
	    bool Login(string userName, string password, ref AccountEntity account);
        bool ChangePassword(long id, string oldPassword,string newPassword);
	    bool CheckResetPassword(string oldPass, string email);
	    bool ResetPassword(string email, string password);
	    AccountEntity GetAccountByEmail(string email);
	}
}
