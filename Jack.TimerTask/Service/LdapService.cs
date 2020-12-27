using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Jack.TimerTask.Container;
using Jack.TimerTask.Service.Dto;


namespace Jack.TimerTask.Service
{
    public class LdapService : ILdapService
    {
        private readonly IOptionsSnapshot<AppSettingConfiguration> _options;

        private readonly IRequestService _requestService;
        public LdapService(IOptionsSnapshot<AppSettingConfiguration> options, IRequestService requestService)
        {
            this._options = options;
            this._requestService = requestService;
        }


        public async Task<GetDeptNodesResponse> GetDepartmentAllAsync(string deptId)
        {
            return await _requestService.RequestPost<GetDeptNodesRequest, GetDeptNodesResponse>(new GetDeptNodesRequest { token = AesEncrypt(_options.Value.LdapService.App_Secret, _options.Value.LdapService.Aes_Key, _options.Value.LdapService.Aes_Iv), deptId = deptId }, _options.Value.LdapService.Url.TrimEnd('/') + "/api/getDeptNodesByCode");
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">加密iv</param>
        /// <returns></returns>
        private string AesEncrypt(string text, string key, string iv)
        {
            try
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.Zeros;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return "";
            }
        }
    }
}
