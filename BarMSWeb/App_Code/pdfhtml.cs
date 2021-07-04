using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for pfdhtml
/// </summary>
public static class pdfhtml
{

    public static string _ProductNames;
    public static string _Size;
    public static string _Qty;
    public static string _SupplierName;
    public static string _comment;
    public static string _deliveryTime;
    public static string _OrderStatus;


    public static string _ProductNamesAll;
    public static string _SizeAll;
    public static string _QtyAll;
    public static string _SupplierNameAll;
    public static string _commentAll;
    public static string _deliveryTimeAll;
    public static string _OrderStatusAll;
    public static string _spSeperator;

    // Supplier Orders

    public static string _InvoiceSum;
    public static string _NonInvoiceSum;
    public static string _TotalOrder;
    public static string _Paid;
    public static string _Due;

    public static string _OrderNo;
    public static string _Date;
    public static string _InvoiceNo;
    public static string _InvSum;
    public static string _NonInvSum;
    public static string _Subtotal;
    public static string _PaidAmount;
    public static string _DueAmount;

    public static string _sProduct;
    public static string _sPacking;
    public static string _sQuantity;
    public static string _sPrice;
    public static string _sVat;
    public static string _sAmount;
    public static string _sSubtotal;
    public static string _OSep;

    public static void SetValues(string pName, string size, string qty, string supplierName, string comment, string deliveryTime, string orderstatus)
    {
        _ProductNames = pName;
        _Size = size;
        _Qty = qty;
        _SupplierName = supplierName;
        _comment = comment;
        _deliveryTime = deliveryTime;
        _OrderStatus = orderstatus;
    }


    public static void SetValuesAll(string pName, string size, string qty, string supplierName, string comment, string deliveryTime, string orderstatus, string seperator)
    {
        _ProductNamesAll = pName;
        _SizeAll = size;
        _QtyAll = qty;
        _SupplierNameAll = supplierName;
        _commentAll = comment;
        _deliveryTimeAll = deliveryTime;
        _OrderStatusAll = orderstatus;
        _spSeperator = seperator;
    }


    public static void SetOrderDetail(string InvoiceSum, string nonInvoiceSum,string SupplierName, string TotalOrder, string Paid, string Due,
                                        string OrderNo, string Date, string InvoceNo, string Invsum, string NonInvSum, string subtoal,
                                        string paidAmount, string DueAmount, string sProduct, string sPacking, string sQuantity, string SPrice,
                                        string sVat, string SAmount, string sSubtotal,string Sep)
    {

        _SupplierName = SupplierName;
        _InvoiceSum = InvoiceSum;
        _NonInvoiceSum = nonInvoiceSum;
        _TotalOrder = TotalOrder;
        _Paid = Paid;
        _Due = Due;

        _OrderNo = OrderNo;
        _Date = Date;
        _InvoiceNo = InvoceNo;
        _InvSum = Invsum;
        _NonInvSum = nonInvoiceSum;
        _Subtotal = subtoal;
        _PaidAmount = paidAmount;
        _DueAmount = DueAmount;

        _sProduct = sProduct;
        _sPacking = sPacking;
        _sQuantity = sQuantity;
        _sPrice = SPrice;
        _sVat = sVat;
        _sAmount = SAmount;
        _sSubtotal = sSubtotal;
        _OSep = Sep;
    }


}
