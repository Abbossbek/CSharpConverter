// Decompiled with JetBrains decompiler
// Type: DocxToHtml.Element.PtExtensions
// Assembly: DocxToHtml, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 969B072E-3738-40E8-B5EB-6A3CF899FBEA
// Assembly location: C:\Users\Abbosbek\.nuget\packages\docxtohtml\1.0.4\lib\netstandard2.0\DocxToHtml.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CSharpConverter.DocxToHtml.Element
{
  public static class PtExtensions
  {
    public static XElement GetXElement(this XmlNode node)
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
        node.WriteTo(writer);
      return xdocument.Root;
    }

    public static XmlNode GetXmlNode(this XElement element)
    {
      XmlDocument xmlNode = new XmlDocument();
      using (XmlReader reader = element.CreateReader())
        xmlNode.Load(reader);
      return (XmlNode) xmlNode;
    }

    public static XDocument GetXDocument(this XmlDocument document)
    {
      XDocument xdocument = new XDocument();
      using (XmlWriter writer = xdocument.CreateWriter())
        document.WriteTo(writer);
      XmlDeclaration xmlDeclaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault<XmlDeclaration>();
      if (xmlDeclaration != null)
        xdocument.Declaration = new XDeclaration(xmlDeclaration.Version, xmlDeclaration.Encoding, xmlDeclaration.Standalone);
      return xdocument;
    }

    public static XmlDocument GetXmlDocument(this XDocument document)
    {
      XmlDocument xmlDocument = new XmlDocument();
      using (XmlReader reader = document.CreateReader())
      {
        xmlDocument.Load(reader);
        if (document.Declaration != null)
        {
          XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration(document.Declaration.Version, document.Declaration.Encoding, document.Declaration.Standalone);
          xmlDocument.InsertBefore((XmlNode) xmlDeclaration, xmlDocument.FirstChild);
        }
      }
      return xmlDocument;
    }

    public static string StringConcatenate(this IEnumerable<string> source) => source.Aggregate<string, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, string, StringBuilder>) ((sb, s) => sb.Append(s)), (Func<StringBuilder, string>) (sb => sb.ToString()));

    public static string StringConcatenate<T>(
      this IEnumerable<T> source,
      Func<T, string> projectionFunc)
    {
      return source.Aggregate<T, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, T, StringBuilder>) ((sb, i) => sb.Append(projectionFunc(i))), (Func<StringBuilder, string>) (sb => sb.ToString()));
    }

    public static IEnumerable<TResult> PtZip<TFirst, TSecond, TResult>(
      this IEnumerable<TFirst> first,
      IEnumerable<TSecond> second,
      Func<TFirst, TSecond, TResult> func)
    {
      using (IEnumerator<TFirst> ie1 = first.GetEnumerator())
      {
        using (IEnumerator<TSecond> ie2 = second.GetEnumerator())
        {
          while (ie1.MoveNext() && ie2.MoveNext())
            yield return func(ie1.Current, ie2.Current);
        }
      }
    }

    public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector)
    {
      TKey key = default (TKey);
      bool haveLast = false;
      List<TSource> source1 = new List<TSource>();
      foreach (TSource source2 in source)
      {
        TSource s = source2;
        TKey k = keySelector(s);
        if (haveLast)
        {
          if (!k.Equals((object) key))
          {
            yield return (IGrouping<TKey, TSource>) new GroupOfAdjacent<TSource, TKey>(source1, key);
            source1 = new List<TSource>() { s };
            key = k;
          }
          else
          {
            source1.Add(s);
            key = k;
          }
        }
        else
        {
          source1.Add(s);
          key = k;
          haveLast = true;
        }
        k = default (TKey);
        s = default (TSource);
      }
      if (haveLast)
        yield return (IGrouping<TKey, TSource>) new GroupOfAdjacent<TSource, TKey>(source1, key);
    }

    private static void InitializeSiblingsReverseDocumentOrder(XElement element)
    {
      XElement xelement = (XElement) null;
      foreach (XElement element1 in element.Elements())
      {
        element1.AddAnnotation((object) new SiblingsReverseDocumentOrderInfo()
        {
          PreviousSibling = xelement
        });
        xelement = element1;
      }
    }

    public static IEnumerable<XElement> SiblingsBeforeSelfReverseDocumentOrder(
      this XElement element)
    {
      if (element.Annotation<SiblingsReverseDocumentOrderInfo>() == null)
        PtExtensions.InitializeSiblingsReverseDocumentOrder(element.Parent);
      XElement xelement = element;
      while (true)
      {
        XElement previousElement = xelement.Annotation<SiblingsReverseDocumentOrderInfo>().PreviousSibling;
        if (previousElement != null)
        {
          yield return previousElement;
          xelement = previousElement;
          previousElement = (XElement) null;
        }
        else
          break;
      }
    }

    private static void InitializeDescendantsReverseDocumentOrder(XElement element)
    {
      XElement xelement = (XElement) null;
      foreach (XElement descendant in element.Descendants())
      {
        descendant.AddAnnotation((object) new DescendantsReverseDocumentOrderInfo()
        {
          PreviousElement = xelement
        });
        xelement = descendant;
      }
    }

    public static IEnumerable<XElement> DescendantsBeforeSelfReverseDocumentOrder(
      this XElement element)
    {
      if (element.Annotation<DescendantsReverseDocumentOrderInfo>() == null)
        PtExtensions.InitializeDescendantsReverseDocumentOrder(element.AncestorsAndSelf().Last<XElement>());
      XElement xelement = element;
      while (true)
      {
        XElement previousElement = xelement.Annotation<DescendantsReverseDocumentOrderInfo>().PreviousElement;
        if (previousElement != null)
        {
          yield return previousElement;
          xelement = previousElement;
          previousElement = (XElement) null;
        }
        else
          break;
      }
    }

    private static void InitializeDescendantsTrimmedReverseDocumentOrder(
      XElement element,
      XName trimName)
    {
      XElement xelement1 = (XElement) null;
      foreach (XElement xelement2 in element.DescendantsTrimmed(trimName))
      {
        xelement2.AddAnnotation((object) new DescendantsTrimmedReverseDocumentOrderInfo()
        {
          PreviousElement = xelement1
        });
        xelement1 = xelement2;
      }
    }

    public static IEnumerable<XElement> DescendantsTrimmedBeforeSelfReverseDocumentOrder(
      this XElement element,
      XName trimName)
    {
      if (element.Annotation<DescendantsTrimmedReverseDocumentOrderInfo>() == null)
        PtExtensions.InitializeDescendantsTrimmedReverseDocumentOrder(element.AncestorsAndSelf(W.txbxContent).FirstOrDefault<XElement>() ?? element.AncestorsAndSelf().Last<XElement>(), trimName);
      XElement xelement = element;
      while (true)
      {
        XElement previousElement = xelement.Annotation<DescendantsTrimmedReverseDocumentOrderInfo>().PreviousElement;
        if (previousElement != null)
        {
          yield return previousElement;
          xelement = previousElement;
          previousElement = (XElement) null;
        }
        else
          break;
      }
    }

    public static string ToStringNewLineOnAttributes(this XElement element)
    {
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        Indent = true,
        OmitXmlDeclaration = true,
        NewLineOnAttributes = true
      };
      StringBuilder sb = new StringBuilder();
      using (StringWriter output = new StringWriter(sb))
      {
        using (XmlWriter writer = XmlWriter.Create((TextWriter) output, settings))
          element.WriteTo(writer);
      }
      return sb.ToString();
    }

    public static IEnumerable<XElement> DescendantsTrimmed(
      this XElement element,
      XName trimName)
    {
      return element.DescendantsTrimmed((Func<XElement, bool>) (e => e.Name == trimName));
    }

    public static IEnumerable<XElement> DescendantsTrimmed(
      this XElement element,
      Func<XElement, bool> predicate)
    {
      Stack<IEnumerator<XElement>> iteratorStack = new Stack<IEnumerator<XElement>>();
      iteratorStack.Push(element.Elements().GetEnumerator());
      while (iteratorStack.Count > 0)
      {
        while (iteratorStack.Peek().MoveNext())
        {
          XElement currentXElement = iteratorStack.Peek().Current;
          if (predicate(currentXElement))
          {
            yield return currentXElement;
          }
          else
          {
            yield return currentXElement;
            iteratorStack.Push(currentXElement.Elements().GetEnumerator());
            currentXElement = (XElement) null;
          }
        }
        iteratorStack.Pop();
      }
    }

    public static IEnumerable<TResult> Rollup<TSource, TResult>(
      this IEnumerable<TSource> source,
      TResult seed,
      Func<TSource, TResult, TResult> projection)
    {
      TResult nextSeed = seed;
      foreach (TSource source1 in source)
      {
        TResult result = projection(source1, nextSeed);
        nextSeed = result;
        yield return result;
      }
    }

    public static IEnumerable<TResult> Rollup<TSource, TResult>(
      this IEnumerable<TSource> source,
      TResult seed,
      Func<TSource, TResult, int, TResult> projection)
    {
      TResult nextSeed = seed;
      int index = 0;
      foreach (TSource source1 in source)
      {
        TResult result = projection(source1, nextSeed, index++);
        nextSeed = result;
        yield return result;
      }
    }

    public static IEnumerable<TSource> SequenceAt<TSource>(
      this TSource[] source,
      int index)
    {
      int i = index;
      while (i < source.Length)
        yield return source[i++];
    }

    public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
    {
      Queue<T> saveList = new Queue<T>();
      int saved = 0;
      foreach (T obj in source)
      {
        if (saved < count)
        {
          saveList.Enqueue(obj);
          ++saved;
        }
        else
        {
          saveList.Enqueue(obj);
          yield return saveList.Dequeue();
        }
      }
    }

    public static bool? ToBoolean(this XAttribute a)
    {
      if (a == null)
        return new bool?();
      string lower = ((string) a).ToLower();
      if (lower == "1")
        return new bool?(true);
      if (lower == "0")
        return new bool?(false);
      if (lower == "true")
        return new bool?(true);
      if (lower == "false")
        return new bool?(false);
      if (lower == "on")
        return new bool?(true);
      return lower == "off" ? new bool?(false) : new bool?((bool) a);
    }

    private static string GetQName(XElement xe)
    {
      string prefixOfNamespace = xe.GetPrefixOfNamespace(xe.Name.Namespace);
      return xe.Name.Namespace == XNamespace.None || prefixOfNamespace == null ? xe.Name.LocalName : prefixOfNamespace + ":" + xe.Name.LocalName;
    }

    private static string GetQName(XAttribute xa)
    {
      string str = xa.Parent != null ? xa.Parent.GetPrefixOfNamespace(xa.Name.Namespace) : (string) null;
      return xa.Name.Namespace == XNamespace.None || str == null ? xa.Name.ToString() : str + ":" + xa.Name.LocalName;
    }

    private static string NameWithPredicate(XElement el)
    {
      if (el.Parent == null || el.Parent.Elements(el.Name).Count<XElement>() == 1)
        return PtExtensions.GetQName(el);
      return PtExtensions.GetQName(el) + "[" + (object) (el.ElementsBeforeSelf(el.Name).Count<XElement>() + 1) + "]";
    }

    public static string StrCat<T>(this IEnumerable<T> source, string separator) => source.Aggregate<T, StringBuilder, string>(new StringBuilder(), (Func<StringBuilder, T, StringBuilder>) ((sb, i) => sb.Append(i.ToString()).Append(separator)), (Func<StringBuilder, string>) (s => s.ToString()));

    public static string GetXPath(this XObject xobj)
    {
      if (xobj.Parent == null)
      {
        switch (xobj)
        {
          case XDocument _:
            return ".";
          case XElement el1:
            return "/" + PtExtensions.NameWithPredicate(el1);
          case XText _:
            return (string) null;
          case XComment xcomment1 when xcomment1.Document != null:
            return "/" + (xcomment1.Document.Nodes().OfType<XComment>().Count<XComment>() != 1 ? "comment()[" + (object) (xcomment1.NodesBeforeSelf().OfType<XComment>().Count<XComment>() + 1) + "]" : "comment()");
          case XProcessingInstruction xprocessingInstruction1:
            return "/" + (xprocessingInstruction1.Document == null || xprocessingInstruction1.Document.Nodes().OfType<XProcessingInstruction>().Count<XProcessingInstruction>() == 1 ? "processing-instruction()" : "processing-instruction()[" + (object) (xprocessingInstruction1.NodesBeforeSelf().OfType<XProcessingInstruction>().Count<XProcessingInstruction>() + 1) + "]");
          default:
            return (string) null;
        }
      }
      else
      {
        switch (xobj)
        {
          case XElement el2:
            return "/" + el2.Ancestors().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + PtExtensions.NameWithPredicate(el2);
          case XAttribute xa when xa.Parent != null:
            return "/" + xa.Parent.AncestorsAndSelf().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + "@" + PtExtensions.GetQName(xa);
          case XComment xcomment2 when xcomment2.Parent != null:
            return "/" + xcomment2.Parent.AncestorsAndSelf().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + (xcomment2.Parent.Nodes().OfType<XComment>().Count<XComment>() != 1 ? "comment()[" + (object) (xcomment2.NodesBeforeSelf().OfType<XComment>().Count<XComment>() + 1) + "]" : "comment()");
          case XCData xcData when xcData.Parent != null:
            return "/" + xcData.Parent.AncestorsAndSelf().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + (xcData.Parent.Nodes().OfType<XText>().Count<XText>() != 1 ? "text()[" + (object) (xcData.NodesBeforeSelf().OfType<XText>().Count<XText>() + 1) + "]" : "text()");
          case XText xtext when xtext.Parent != null:
            return "/" + xtext.Parent.AncestorsAndSelf().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + (xtext.Parent.Nodes().OfType<XText>().Count<XText>() != 1 ? "text()[" + (object) (xtext.NodesBeforeSelf().OfType<XText>().Count<XText>() + 1) + "]" : "text()");
          case XProcessingInstruction xprocessingInstruction2 when xprocessingInstruction2.Parent != null:
            return "/" + xprocessingInstruction2.Parent.AncestorsAndSelf().InDocumentOrder<XElement>().Select<XElement, string>((Func<XElement, string>) (e => PtExtensions.NameWithPredicate(e))).StrCat<string>("/") + (xprocessingInstruction2.Parent.Nodes().OfType<XProcessingInstruction>().Count<XProcessingInstruction>() != 1 ? "processing-instruction()[" + (object) (xprocessingInstruction2.NodesBeforeSelf().OfType<XProcessingInstruction>().Count<XProcessingInstruction>() + 1) + "]" : "processing-instruction()");
          default:
            return (string) null;
        }
      }
    }
  }
}
