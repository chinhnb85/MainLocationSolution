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

namespace ModelCMS.User
{
    public class IplUser : BaseIpl<ADOProvider>, IUser
    {
        #region Methods

        /// <summary>
        /// Saves a record to the User table.
        /// </summary>
        public long Insert(UserEntity obj)
        {
            long res = 0;
            //bool flag = false;
            try
            {
                var p = Param(obj);
                var flag = unitOfWork.ProcedureExecute("Sp_User_Insert", p);
                res = flag ? p.Get<long>("@Id") : 0;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
            return res;
        }

        /// <summary>
        /// Updates a record in the User table.
        /// </summary>
        public bool Update(UserEntity obj)
        {
            try
            {
                var p = Param(obj, "edit");
                var res = unitOfWork.ProcedureExecute("Sp_User_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the User table by its primary key.
        /// </summary>
        public bool Delete(long iD, long adminId, int adminType)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@AccID", iD);
                p.Add("@AdminId", adminId);
                p.Add("@AdminType", adminType);
                var res = unitOfWork.ProcedureExecute("Sp_User_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public bool UpdateAvatar(UserEntity obj)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", obj.Id);                
                var res = unitOfWork.ProcedureExecute("Sp_User_UpdateAvatar", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public UserEntity GetUserByEmail(string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@email", email);
                var data = unitOfWork.Procedure<UserEntity>("Sp_User_GetUserByEmail", p).FirstOrDefault();
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
                unitOfWork.ProcedureExecute("Sp_User_CheckUsernameExist", p);
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
                p.Add("@Id", id);
                p.Add("@Res", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_User_CheckEmailExist", p);
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
                unitOfWork.ProcedureExecute("Sp_User_CheckResetPassword", p);
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
                unitOfWork.ProcedureExecute("Sp_User_ResetPassword", p);
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
        /// Selects a single record from the User table.
        /// </summary>
        public UserEntity ViewDetail(string id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<UserEntity>("Sp_User_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool Login(string userName, string password, ref UserEntity obj)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@userName", userName);
                p.Add("@password", password);
                var data = unitOfWork.Procedure<UserEntity>("Sp_User_Login", p).SingleOrDefault();                
                if (data != null)
                {
                    obj = data;
                    return true;
                }
                else
                {
                    obj = null;
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
                var data = unitOfWork.Procedure<string>("Sp_User_ChangePassword", p).SingleOrDefault();
                return data == "SUCCESS";

            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the User table.
        /// </summary>
        public List<UserEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<UserEntity>("Sp_User_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the User table.
        /// </summary>
        public List<UserEntity> ListAllPaging(UserEntity userInfo, int pageIndex, int pageSize, string sortColumn, string sortDesc, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@username", userInfo.UserName);                
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@sortColumn", sortColumn);
                p.Add("@sortDesc", sortDesc);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<UserEntity>("Sp_User_ListAllPaging", p);
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
        /// Saves a record to the User table.
        /// </summary>
        private DynamicParameters Param(UserEntity obj, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@UserType", obj.UserType);
                p.Add("@UserName", obj.UserName);
                p.Add("@Password", obj.Password);
                p.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            }
            else if(action == "edit")
            {
                p.Add("@Id", obj.Id);
            }                       

            return p;
        }


        #endregion
    }
}
