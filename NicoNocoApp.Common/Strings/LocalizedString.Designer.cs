﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NicoNocoApp.Common.Strings {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LocalizedString {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizedString() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NicoNocoApp.Common.Strings.LocalizedString", typeof(LocalizedString).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   DM に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string DirectMessage {
            get {
                return ResourceManager.GetString("DirectMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Reply に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Mensions {
            get {
                return ResourceManager.GetString("Mensions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Retweet by {0} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string RetweetBy {
            get {
                return ResourceManager.GetString("RetweetBy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Settings に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Settings {
            get {
                return ResourceManager.GetString("Settings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Stream に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Stream {
            get {
                return ResourceManager.GetString("Stream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Timeline に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Timeline {
            get {
                return ResourceManager.GetString("Timeline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Tweet に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string Tweet {
            get {
                return ResourceManager.GetString("Tweet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0} / @{1} に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string UserLabelFormat {
            get {
                return ResourceManager.GetString("UserLabelFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   What you doing ? に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string WhatYouDoing {
            get {
                return ResourceManager.GetString("WhatYouDoing", resourceCulture);
            }
        }
    }
}
