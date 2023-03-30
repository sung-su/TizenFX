/**
 *Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file StatePropertyDefinitionCollection.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 10.10.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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


using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Class for calling UpdateStateProperty() of Component through ICollection interface.
    /// It does not store any item.
    /// This class is only intended for use by the XAML Application.
    /// </summary>
    /// <code>
    /// Refer to XAML samples
    /// </code>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class StatePropertyDefinitionCollection : Collection<StatePropertyDefinition>
    {
        /// <summary>
        /// constructor of StatePropertyDefinitionCollection
        /// </summary>
        /// <param name="owner"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public StatePropertyDefinitionCollection(Component owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("the arugment 'owner' is null");
            }

            this.owner = owner;
        }

        /// <summary>
        /// Occurs when an item is going to be added or inserted.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        protected override void InsertItem(int index, StatePropertyDefinition item)
        {
            owner.UpdateStateProperty(item.StateName, item.PropertyName, item.PropertyValue);
            // Do not call base.InsertItem() because the item will not be used again.
        }

        /// <summary>
        /// owner Component
        /// </summary>
        protected Component owner;
    }
}