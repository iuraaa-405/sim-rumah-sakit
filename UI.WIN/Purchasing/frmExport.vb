Imports System.IO
Imports DataAccess

Imports System.Linq

Public Class frmExport
    Private sNoId As String
    Private sSheetName As String

    Public Sub fn_LoadMe(Optional ByVal noID As String = "")
        sNoId = noID
    End Sub

    Private Sub frmExport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
    End Sub

    Public Sub fn_Load()
        Try
            grd.DataSource = Nothing

            Dim oPurchaseInvoice As New Purchasing.clsPurchaseInvoice

            Dim ds = (From x In oPurchaseInvoice.GetDataDetail _
                      Where x.KDPI = sNoId _
                     Order By x.M_ITEM.NMITEM1 Ascending _
                     Select x.M_ITEM.NMITEM1, x.M_ITEM.NMITEM2, HARGA = x.M_ITEM.M_ITEM_UOMs.FirstOrDefault().PRICESALESSTANDARD, Quantity = x.QTY)

            ds = ds.Where(Function(x) x.Quantity <> 0)

            grd.DataSource = ds.ToList()
        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim xls As New Microsoft.Office.Interop.Excel.Application

            xls.Workbooks.Add()
            xls.Visible = True

            xls.Range("A1").Offset(0, 0).Value = "NMITEM1"
            xls.Range("B1").Offset(0, 0).Value = "NMITEM2"
            xls.Range("C1").Offset(0, 0).Value = "HARGA"

            Dim sNumber As Integer = 0

            For iLoop As Integer = 0 To grv.RowCount - 1
                For xLoop As Integer = 0 To grv.GetRowCellValue(iLoop, "Quantity") - 1
                    xls.Range("A2").Offset(sNumber, 0).Value = grv.GetRowCellValue(iLoop, "NMITEM1")
                    xls.Range("B2").Offset(sNumber, 0).Value = grv.GetRowCellValue(iLoop, "NMITEM2")
                    xls.Range("C2").Offset(sNumber, 0).Value = grv.GetRowCellValue(iLoop, "HARGA")

                    sNumber += 1
                Next
            Next

            xls.ScreenUpdating = False
            xls.Columns.AutoFit()
            'xls.AutoFilter()
            'xls.AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple)
            xls.ScreenUpdating = True
        Catch oErr As Exception
            MsgBox("Export Excel : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class