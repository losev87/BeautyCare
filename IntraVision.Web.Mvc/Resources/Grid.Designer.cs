﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntraVision.Web.Mvc.Resources {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Grid {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Grid() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IntraVision.Web.Mvc.Resources.Grid", typeof(Grid).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to No data available.
        /// </summary>
        public static string EmptyText {
            get {
                return ResourceManager.GetString("EmptyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must call Prepare method directly before using the grid..
        /// </summary>
        public static string GridMustBePrepared {
            get {
                return ResourceManager.GetString("GridMustBePrepared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to first.
        /// </summary>
        public static string PaginationFirst {
            get {
                return ResourceManager.GetString("PaginationFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Showing {0} - {1} of {2}.
        /// </summary>
        public static string PaginationFormat {
            get {
                return ResourceManager.GetString("PaginationFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to last.
        /// </summary>
        public static string PaginationLast {
            get {
                return ResourceManager.GetString("PaginationLast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to next.
        /// </summary>
        public static string PaginationNext {
            get {
                return ResourceManager.GetString("PaginationNext", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to next {0}.
        /// </summary>
        public static string PaginationNextN {
            get {
                return ResourceManager.GetString("PaginationNextN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to prev.
        /// </summary>
        public static string PaginationPrev {
            get {
                return ResourceManager.GetString("PaginationPrev", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to prev {0}.
        /// </summary>
        public static string PaginationPrevN {
            get {
                return ResourceManager.GetString("PaginationPrevN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Showing {0} of {1}.
        /// </summary>
        public static string PaginationSingleFormat {
            get {
                return ResourceManager.GetString("PaginationSingleFormat", resourceCulture);
            }
        }
    }
}