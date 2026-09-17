Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports QRCoder
Public Class xtraReportEMedrekRI_13
    Dim oIdentitas As New Identitas.clsIdentitasPasien
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim dsIdentitas = oIdentitas.GetData(GetCurrentColumnValue("KDKUNJUNGAN"))

        If dsIdentitas IsNot Nothing Then
            tbclNama.Text = dsIdentitas.NAMAPASIEN
            tbclNORM.Text = dsIdentitas.KDCUSTOMER
            tbclNOREG.Text = dsIdentitas.KDPENDAFTARAN
            tbclUMUR.Text = dsIdentitas.USIA
            tbclJK.Text = dsIdentitas.JENISKELAMIN
            tbclRUANGAN.Text = dsIdentitas.TUJUAN
            tbclKELAS.Text = dsIdentitas.KELASPELAYANAN
        End If
    End Sub
    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        sTandaTanganDokter = GetCurrentColumnValue("KDUSER")
        Try
            If sTandaTanganDokter <> "" Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganDokter, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picDokter.Image = code.GetGraphic(6)
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class