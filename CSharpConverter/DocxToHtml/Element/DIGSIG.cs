// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.DIGSIG
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class DIGSIG
  {
    public static readonly XNamespace digsig = (XNamespace) "http://schemas.microsoft.com/office/2006/digsig";
    public static readonly XName ApplicationVersion = DIGSIG.digsig + nameof (ApplicationVersion);
    public static readonly XName ColorDepth = DIGSIG.digsig + nameof (ColorDepth);
    public static readonly XName HorizontalResolution = DIGSIG.digsig + nameof (HorizontalResolution);
    public static readonly XName ManifestHashAlgorithm = DIGSIG.digsig + nameof (ManifestHashAlgorithm);
    public static readonly XName Monitors = DIGSIG.digsig + nameof (Monitors);
    public static readonly XName OfficeVersion = DIGSIG.digsig + nameof (OfficeVersion);
    public static readonly XName SetupID = DIGSIG.digsig + nameof (SetupID);
    public static readonly XName SignatureComments = DIGSIG.digsig + nameof (SignatureComments);
    public static readonly XName SignatureImage = DIGSIG.digsig + nameof (SignatureImage);
    public static readonly XName SignatureInfoV1 = DIGSIG.digsig + nameof (SignatureInfoV1);
    public static readonly XName SignatureProviderDetails = DIGSIG.digsig + nameof (SignatureProviderDetails);
    public static readonly XName SignatureProviderId = DIGSIG.digsig + nameof (SignatureProviderId);
    public static readonly XName SignatureProviderUrl = DIGSIG.digsig + nameof (SignatureProviderUrl);
    public static readonly XName SignatureText = DIGSIG.digsig + nameof (SignatureText);
    public static readonly XName SignatureType = DIGSIG.digsig + nameof (SignatureType);
    public static readonly XName VerticalResolution = DIGSIG.digsig + nameof (VerticalResolution);
    public static readonly XName WindowsVersion = DIGSIG.digsig + nameof (WindowsVersion);
  }
}
