/// @file SecurityUtil.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
/// PROPRIETARY/CONFIDENTIAL 
/// This software is the confidential and proprietary
/// information of SAMSUNG ELECTRONICS ("Confidential Information"). You shall
/// not disclose such Confidential Information and shall use it only in
/// accordance with the terms of the license agreement you entered into with
/// SAMSUNG ELECTRONICS. SAMSUNG make no representations or warranties about the
/// suitability of the software, either express or implied, including but not
/// limited to the implied warranties of merchantability, fitness for a
/// particular purpose, or non-infringement. SAMSUNG shall not be liable for any
/// damages suffered by licensee as a result of using, modifying or distributing
/// this software or its derivatives.

//TODO: Need to check if this is needed
//using Tizen.TV.Security;

namespace Tizen.NUI.FLUX
{
    internal class SecurityUtil
    {
        static bool platformPrivilegeChecked = false;
        public static void CheckPlatformPrivileges()
        {
            if (platformPrivilegeChecked == true)
            { 
                return;
            }

            CheckSpecificPrivileges("http://tizen.org/privilege/internal/default/platform");

            platformPrivilegeChecked = true;                        
        }

        public static void CheckSpecificPrivileges(string privilege)
        {
            //TODO: Below code is commented out because it is not supported in DA
            //string smackLabel;
            //string uid;
            //string clientSession = "";

            //// Cynara  structure init
            //if (Security.Privilege.Cynara.CynaraInitialize(false) != 0)
            //{
            //    /* error : Failed to initialize cynara structure*/
            //    throw new Exception("Cynara Initialization Fail");
            //}

            //try
            //{
            //    // Get credential – Smack label            
            //    smackLabel = global::System.IO.File.ReadAllText("/proc/self/attr/current");
            //    // Get credential – UID            
            //    uid = Security.Privilege.Cynara.GetUid();

            //    // Cynara check            
            //    if (Security.Privilege.Cynara.CynaraCheck(smackLabel, clientSession, uid, privilege) != 2)
            //    {
            //        throw new MethodAccessException("You should add this privilege (" + privilege + ") in your app");
            //    }

            //}
            //catch (ObjectDisposedException e)
            //{
            //    /* error case : Not Initialize Cynara */
            //    throw new Exception($"Fail to initialize Cynara : {e}");
            //}
            //finally
            //{
            //    Security.Privilege.Cynara.CynaraFinish();
            //}
        }
    }
}
