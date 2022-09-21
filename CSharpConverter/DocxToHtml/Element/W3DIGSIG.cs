// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.W3DIGSIG
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class W3DIGSIG
  {
    public static readonly XNamespace w3digsig = (XNamespace) "http://www.w3.org/2000/09/xmldsig#";
    public static readonly XName CanonicalizationMethod = W3DIGSIG.w3digsig + nameof (CanonicalizationMethod);
    public static readonly XName DigestMethod = W3DIGSIG.w3digsig + nameof (DigestMethod);
    public static readonly XName DigestValue = W3DIGSIG.w3digsig + nameof (DigestValue);
    public static readonly XName Exponent = W3DIGSIG.w3digsig + nameof (Exponent);
    public static readonly XName KeyInfo = W3DIGSIG.w3digsig + nameof (KeyInfo);
    public static readonly XName KeyValue = W3DIGSIG.w3digsig + nameof (KeyValue);
    public static readonly XName Manifest = W3DIGSIG.w3digsig + nameof (Manifest);
    public static readonly XName Modulus = W3DIGSIG.w3digsig + nameof (Modulus);
    public static readonly XName Object = W3DIGSIG.w3digsig + nameof (Object);
    public static readonly XName Reference = W3DIGSIG.w3digsig + nameof (Reference);
    public static readonly XName RSAKeyValue = W3DIGSIG.w3digsig + nameof (RSAKeyValue);
    public static readonly XName Signature = W3DIGSIG.w3digsig + nameof (Signature);
    public static readonly XName SignatureMethod = W3DIGSIG.w3digsig + nameof (SignatureMethod);
    public static readonly XName SignatureProperties = W3DIGSIG.w3digsig + nameof (SignatureProperties);
    public static readonly XName SignatureProperty = W3DIGSIG.w3digsig + nameof (SignatureProperty);
    public static readonly XName SignatureValue = W3DIGSIG.w3digsig + nameof (SignatureValue);
    public static readonly XName SignedInfo = W3DIGSIG.w3digsig + nameof (SignedInfo);
    public static readonly XName Transform = W3DIGSIG.w3digsig + nameof (Transform);
    public static readonly XName Transforms = W3DIGSIG.w3digsig + nameof (Transforms);
    public static readonly XName X509Certificate = W3DIGSIG.w3digsig + nameof (X509Certificate);
    public static readonly XName X509Data = W3DIGSIG.w3digsig + nameof (X509Data);
    public static readonly XName X509IssuerName = W3DIGSIG.w3digsig + nameof (X509IssuerName);
    public static readonly XName X509SerialNumber = W3DIGSIG.w3digsig + nameof (X509SerialNumber);
  }
}
