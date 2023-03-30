/// @file KeyCombinations.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Enumeration for combination key to special case. 
    /// </summary>
    public enum KeyCombinations : uint
    {
        /// <summary> Entry factory (ATSC) </summary>
        Mute182Power = 1,
        /// <summary> Entry factory (DVB,PAL)</summary>
        InfoMenuMutePower,
        /// <summary> </summary>
        MuteEnterMuteEnter,
        /// <summary> </summary>
        Mute948Exit,
        /// <summary> Change RS-232C Option </summary>
        Mute369Exit,
        /// <summary> Change option of Watch Dog : "Off" </summary>
        Mute258Exit,
        /// <summary> Launch factory application </summary>
        InfoFactory,
        /// <summary> </summary>
        SourceExitPower,
        /// <summary> Wallmount Test </summary>
        Mute186Exit,
        /// <summary> Network test </summary>
        Mute782Exit,
        /// <summary> </summary>
        Mute783Exit,
        /// <summary> </summary>
        Mute737Enter,
        /// <summary> </summary>
        Mute437Exit,
        /// <summary> Launch hotel factory application </summary>
        Mute119Enter,
        /// <summary> Add Korean language in menu </summary>
        Mute567Exit,
        /// <summary> Print main chip pattern </summary>
        Mute147Mute,
        /// <summary> Print T-Con chip pattern </summary>
        Mute369Mute,
        /// <summary> Next operation exit after the service reset.</summary>
        InfoMute,
        /// <summary> </summary>
        Mute227Exit,
        /// <summary> </summary>
        InfoSource,
        /// <summary> </summary>
        MuteRSS,
        /// <summary> CI Plus Key write </summary>
        Mute247Exit,
        /// <summary> CI Plus Key write (Product Key write) </summary>
        Mute207Exit,
        /// <summary> CI Plus Key write (Development Key write) </summary>
        Mute209Exit,
        /// <summary> </summary>
        Mute0Mute0,
        /// <summary> </summary>
        Mute564Exit,
        /// <summary> CEC TX/RX test(Not supported(using 13's))  </summary>
        Mute184Exit,
        /// <summary> </summary>
        FF287RW,
        /// <summary> Password Reset </summary>
        Mute824Power,
        /// <summary> </summary>
        MuteVolUpReturnVolDownReturnVolUpReturn,
        /// <summary> </summary>
        Mute579Exit,
        /// <summary> Not supported(using 13's) </summary>
        FF289RW,
        /// <summary> Not supported(using 13's) </summary>
        FF2891,
        /// <summary> Camera/Mic Test </summary>
        Mute569Exit,
        /// <summary> Update SERET </summary>
        InfoMenuMuteMenuExit,
        /// <summary> Dual View to Set 2 Screen on TV </summary>
        Mute747Exit,
        /// <summary> Dual View to Set 2 Screen on HDMI </summary>
        Mute787Exit,
        /// <summary> </summary>
        FF2892,
        /// <summary> </summary>
        FF182RW,
        /// <summary> </summary>
        Mute987Exit,
        /// <summary> </summary>
        MuteMuteExitExitExit,
        /// <summary> </summary>
        MutePlayPlayPlayExit,
        /// <summary> </summary>
        MuteStopStopStopExit,
        /// <summary> </summary>
        MuteReturnVolUpChannelUpMute,
        /// <summary> </summary>
        InfoStop,
        /// <summary> </summary>
        Mute9900Exit,
        /// <summary> </summary>
        Mute3Mute3,
        /// <summary> </summary>
        Mute7Mute7,
        /// <summary> </summary>
        InfoExitMute,
        /// <summary> </summary>
        Keysource2589Keyprech,
        /// <summary> Wireless Network BT Function Off to Reaction </summary>
        FF2006201409,
        /// <summary> Wireless Network BT Function On to Reaction </summary>
        FF2006201408,
        /// <summary> Wireless Network WIFI Function Off to Reaction </summary>
        FF2006201509,
        /// <summary> Not Support(using 13's) </summary>
        MuteVolUpChannelUpMute,
        /// <summary> </summary>
        Mute007Exit,
        /// <summary> </summary>
        Mute487Exit,
        /// <summary> </summary>
        Mute482Exit,
        /// <summary> Change the Background Color step by step in a floating state.(White, Blue, Black) </summary>
        Info533Exit,
        /// <summary> Temporary Kept to mantain compatibility </summary>
        Mute777Exit,
        /// <summary> To Display status of History Trace </summary>
        Mute489Exit,
        /// <summary> Skip a Test , in PNP Testing </summary>
        KeyTest09,
        /// <summary> To get ID of the contents protection module and Version information. </summary>
        Mute753Exit,
        /// <summary> To set the Wi-Fi related information afloat on TV in the hidden key input through UI </summary>
        Mute777,
        /// <summary> Enter WIFI test mode </summary>
        FF2006201609,
        /// <summary> CX-Core stop mode, will be removed </summary>
        Mute7193Exit,
        /// <summary> CX-Core stop mode </summary>
        MuteUpLeftUpDownMuteBack,
        /// <summary> Factory voice function </summary>
        FFPlayPauseRW,
        /// <summary> Menu for Exhibition mode </summary>
        FactoryEnterInfo,
        /// <summary> launch LFD software upgrade download progress UI </summary>
        Mute911Exit,
        /// <summary> launch Remote JackPack Port Test for Hotel TV </summary>
        Mute189Exit,
        /// <summary> launch Hotel Port Test </summary>
        Mute188Exit,
        /// <summary> Auto detection on/off </summary>
        Mute729Exit,
        /// <summary> Auto detection on/off using smart control </summary>
        MuteVolDownChannelDownMute,
        /// <summary> launch task manager application </summary>
        Mute114Exit,
        /// <summary> launch local dimming on automation production </summary>
        Mute419Exit,
        /// <summary> start tcp dump utility </summary>
        Mute135Mute,
        /// <summary> stop tcp dump utility </summary>
        Mute246Mute,
        /// <summary> key debug mode ON  (keyrouter-tv only) </summary>
        Mute000Mute,
        /// <summary> key debug mode OFF (keyrouter-tv only) </summary>
        Mute111Mute,
        /// <summary> Max Value </summary>
        Max
    }
}