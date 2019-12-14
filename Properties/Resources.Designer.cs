// Decompiled with JetBrains decompiler
// Type: QFlashKit.Properties.Resources
// Assembly: QFlashKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC1A28B0-F0EB-4D8A-87A7-1C7E69B878B5
// Assembly location: C:\XiaoMi\QFlashKit\QFlashKit.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace QFlashKit.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Resources.resourceMan, (object) null))
          Resources.resourceMan = new ResourceManager("QFlashKit.Properties.Resources", typeof (Resources).Assembly);
        return Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Resources.resourceCulture;
      }
      set
      {
        Resources.resourceCulture = value;
      }
    }

    internal static string txtPath
    {
      get
      {
        return Resources.ResourceManager.GetString("txtPath", Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
