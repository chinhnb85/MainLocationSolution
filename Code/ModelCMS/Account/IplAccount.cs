using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LibCore.Data;
using LibCore.Helper.Extensions;
using LibCore.Helper.Logging;
using ModelCMS.Base;

namespace ModelCMS.Account
{
    public class IplAccount : BaseIpl<ADOProvider>, IAccount
    {
        #region Methods

        /// <summary>
        /// Saves a record to the Account table.
        /// </summary>
        public long Insert(AccountEntity account)
        {
            long res = 0;
            //bool flag = false;
            try
            {
                var p = Param(account);
                var flag = unitOfWork.ProcedureExecute("Sp_Account_Insert", p);
                res = flag ? p.Get<long>("@ID") : 0;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
            return res;
        }

        /// <summary>
        /// Updates a record in the Account table.
        /// </summary>
        public bool Update(AccountEntity account)
        {
            try
            {
                var p = Param(account, "edit");
                var res = unitOfWork.ProcedureExecute("Sp_Account_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the Account table by its primary key.
        /// </summary>
        public bool Delete(long iD, long adminId, int adminType)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@AccID", iD);
                p.Add("@AdminId", adminId);
                p.Add("@AdminType", adminType);
                var res = unitOfWork.ProcedureExecute("Sp_Account_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public bool UpdateAvatar(AccountEntity account)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ID", account.ID);
                p.Add("@Photo", account.Photo);
                var res = unitOfWork.ProcedureExecute("Sp_Account_UpdateAvatar", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public AccountEntity GetAccountByEmail(string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@email", email);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_GetAccountByEmail", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        /// <summary>
        /// Check username exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUsernameExist(string username)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserName", username);
                p.Add("@Res", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckUsernameExist", p);
                var data = p.Get<bool>("@Res");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool CheckEmailExist(string email, long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);
                p.Add("@ID", id);
                p.Add("@Res", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckEmailExist", p);
                var data = p.Get<bool>("@Res");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool CheckResetPassword(string oldPass, string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@oldPass", oldPass);
                p.Add("@email", email);
                p.Add("@data", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckResetPassword", p);
                var data = p.Get<bool>("@data");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool ResetPassword(string email, string password)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@email", email);
                p.Add("@password", password);
                p.Add("@data", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_ResetPassword", p);
                var data = p.Get<bool>("@data");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// Selects a single record from the Account table.
        /// </summary>
        public AccountEntity ViewDetail(string id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@ID", id);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool Login(string userName, string password, ref AccountEntity account)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@userName", userName);
                p.Add("@password", password);
                var data = unitOfWork.ProcedureQueryMulti("Sp_Account_Login", p);
                var result = data.Read<string>().SingleOrDefault();
                if (result == "SUCCESS")
                {
                    account = data.Read<AccountEntity>().SingleOrDefault();
                    return true;
                }
                else
                {
                    account = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        public bool ChangePassword(long id, string oldPassword, string newPassword)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                p.Add("@OldPassword", oldPassword);
                p.Add("@Password", newPassword);
                var data = unitOfWork.Procedure<string>("Sp_Account_ChangePassword", p).SingleOrDefault();
                return data == "SUCCESS";

            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the Account table.
        /// </summary>
        public List<AccountEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the Account table.
        /// </summary>
        public List<AccountEntity> ListAllPaging(AccountEntity userInfo, int pageIndex, int pageSize, string sortColumn, string sortDesc, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@username", userInfo.UserName);
                p.Add("@firstname", userInfo.FirstName);
                p.Add("@lastname", userInfo.LastName);
                p.Add("@email", userInfo.Email);
                p.Add("@syndicname", userInfo.SyndicName);
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@sortColumn", sortColumn);
                p.Add("@sortDesc", sortDesc);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves a record to the Account table.
        /// </summary>
        private DynamicParameters Param(AccountEntity account, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@AdminType", account.AdminType);
                p.Add("@UserName", account.UserName);
                p.Add("@Password", account.Password);
                p.Add("@ID", dbType: DbType.Int64, direction: ParameterDirection.Output);
            }
            else if(action == "edit")
            {
                p.Add("@ID", account.ID);
            }
            
            p.Add("@FirstName", account.FirstName);
            p.Add("@LastName", account.LastName);
            p.Add("@Address", account.Address);
            p.Add("@CodePostal", account.CodePostal);
            p.Add("@City", account.City);
            p.Add("@Country", account.Country);
            //p.Add("@BirthCity", account.BirthCity);
            //p.Add("@BirthCountry", account.BirthCountry);
            p.Add("@Phone", account.Phone);
            p.Add("@Phone2", account.Phone2);
            p.Add("@Email", account.Email);
            //p.Add("@DateOfBirth", account.DateOfBirth);
            //p.Add("@PlaceOfBirth", account.PlaceOfBirth);
            p.Add("@Gender", account.Gender);

            return p;
        }


        #endregion
    }
}
