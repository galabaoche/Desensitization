using Desensitization.Desensitize;
using Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Controllers
{
    public class AccountContactController : ControllerBase
    {
        public ActionResult Index()
        {
            IList<AccountContactDto> contactList = new List<AccountContactDto>()
            {
                new AccountContactDto{
                    Contact="03135368749",
                    DesensitizeContact="03135368749",
                    FactTypeName="固话",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="张",
                    ContactTypeName="固话",
                },
                new AccountContactDto{
                    Contact="15210367416",
                    DesensitizeContact="15210367416",
                    FactTypeName="手机",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="李宇航",
                    ContactTypeName="手机",
                },
                new AccountContactDto{
                    Contact="1275706978@qq.com",
                    DesensitizeContact="1275706978@qq.com",
                    FactTypeName="Email",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="范小刚",
                    ContactTypeName="Email",
                },
                new AccountContactDto{
                    Contact="12@qq.com",
                    DesensitizeContact="12@qq.com",
                    FactTypeName="Email",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="李小刚",
                    ContactTypeName="Email",
                },
                 new AccountContactDto{
                    Contact="123@qq.com",
                    DesensitizeContact="123@qq.com",
                    FactTypeName="Email",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="郝小刚",
                    ContactTypeName="Email",
                },
                new AccountContactDto{
                    Contact="1245678",
                    DesensitizeContact="1245678",
                    FactTypeName="微信",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="白小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="1245678456",
                    DesensitizeContact="1245678456",
                    FactTypeName="微信",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="楚小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="1245678456345",
                    DesensitizeContact="1245678456345",
                    FactTypeName="微信",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="赵小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="黑山市黑山县",
                    DesensitizeContact="黑山市黑山县",
                    FactTypeName="其他",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="魏小刚",
                    ContactTypeName="其他",
                },
                new AccountContactDto{
                    Contact="此单位名称未知",
                    DesensitizeContact="此单位名称未知",
                    FactTypeName="单位名称",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="朱小刚",
                    ContactTypeName="单位名称",
                },
                new AccountContactDto{
                    Contact="此地址是瞎写的",
                    DesensitizeContact="此地址是瞎写的",
                    FactTypeName="地址",
                    CustomerId=67,
                    CustomerName="浦发",
                    ContactName="朱小刚",
                    ContactTypeName="地址",
                },

                new AccountContactDto{
                    Contact="03135368749",
                    DesensitizeContact="03135368749",
                    FactTypeName="固话",
                    CustomerId=78,
                    CustomerName="浦发",
                    ContactName="张",
                    ContactTypeName="固话",
                },
                new AccountContactDto{
                    Contact="15210367416",
                    DesensitizeContact="15210367416",
                    FactTypeName="手机",
                    CustomerId=77,
                    CustomerName="浦发",
                    ContactName="李宇航",
                    ContactTypeName="手机",
                },
                new AccountContactDto{
                    Contact="1275706978@qq.com",
                    DesensitizeContact="1275706978@qq.com",
                    FactTypeName="Email",
                    CustomerId=76,
                    CustomerName="浦发",
                    ContactName="范小刚",
                    ContactTypeName="Email",
                },
                new AccountContactDto{
                    Contact="12@qq.com",
                    DesensitizeContact="12@qq.com",
                    FactTypeName="Email",
                    CustomerId=75,
                    CustomerName="浦发",
                    ContactName="李小刚",
                    ContactTypeName="Email",
                },
                 new AccountContactDto{
                    Contact="123@qq.com",
                    DesensitizeContact="123@qq.com",
                    FactTypeName="Email",
                    CustomerId=74,
                    CustomerName="浦发",
                    ContactName="郝小刚",
                    ContactTypeName="Email",
                },
                new AccountContactDto{
                    Contact="1245678",
                    DesensitizeContact="1245678",
                    FactTypeName="微信",
                    CustomerId=73,
                    CustomerName="浦发",
                    ContactName="白小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="1245678456",
                    DesensitizeContact="1245678456",
                    FactTypeName="微信",
                    CustomerId=72,
                    CustomerName="浦发",
                    ContactName="楚小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="1245678456345",
                    DesensitizeContact="1245678456345",
                    FactTypeName="微信",
                    CustomerId=71,
                    CustomerName="浦发",
                    ContactName="赵小刚",
                    ContactTypeName="微信",
                },
                new AccountContactDto{
                    Contact="黑山市黑山县",
                    DesensitizeContact="黑山市黑山县",
                    FactTypeName="其他",
                    CustomerId=70,
                    CustomerName="浦发",
                    ContactName="魏小刚",
                    ContactTypeName="其他",
                },
                new AccountContactDto{
                    Contact="此单位名称未知",
                    DesensitizeContact="此单位名称未知",
                    FactTypeName="单位名称",
                    CustomerId=69,
                    CustomerName="浦发",
                    ContactName="朱小刚",
                    ContactTypeName="单位名称",
                },
                new AccountContactDto{
                    Contact="此地址是瞎写的",
                    DesensitizeContact="此地址是瞎写的",
                    FactTypeName="地址",
                    CustomerId=930679933297131520,
                    CustomerName="小赢普惠",
                    ContactName="朱小刚",
                    ContactTypeName="身份证地址",
                },
            };
            //contactList.Desensitizate();
            return Desensitizate(contactList);
        }
    }
}