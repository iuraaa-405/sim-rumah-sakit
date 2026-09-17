'Imports iPOS.DA.dcEntity

'Imports System.IO
'Imports iTextSharp.text
'Imports iTextSharp.text.pdf

Namespace Sales
    Public Class clsUmpanBalikBPJS
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_UMPANBALIK
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_UMPANBALIK
        End Function
        Public Function GetData() As List(Of S_UMPANBALIK)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_UMPANBALIKs.OrderBy(Function(x) x.SEP).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_UMPANBALIK
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_UMPANBALIKs.FirstOrDefault(Function(x) x.SEP = sParameter)
        End Function
        Public Function GetStructureDetailList() As List(Of S_UMPANBALIK)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_UMPANBALIK)
        End Function
        Public Function GetStructureDetail() As S_UMPANBALIK
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_UMPANBALIK
        End Function
        'Public Function GetDataPendaftaranBySEP(ByVal sParameter As String) As S_PENDAFTARAN_H
        '    If Not oConnection.GetConnection Then
        '        GetDataPendaftaranBySEP = Nothing
        '        Exit Function
        '    End If
        '    GetDataPendaftaranBySEP = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.SEP = sParameter)
        'End Function
        'Public Function GetDataPendaftaranByRM(ByVal sRM As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
        '    If Not oConnection.GetConnection Then
        '        GetDataPendaftaranByRM = Nothing
        '        Exit Function
        '    End If


        '    GetDataPendaftaranByRM = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.DATE >= sDATE.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDATE.ToString("yyyy-MM-dd") & " 23:59:59" And x.KDCUSTOMER = sRM.PadLeft(9, "0"))

        'End Function
        'Public Function GetDataPendaftaranByRMList() As List(Of S_PENDAFTARAN_H)
        '    If Not oConnection.GetConnection Then
        '        GetDataPendaftaranByRMList = Nothing
        '    End If
        '    GetDataPendaftaranByRMList = New List(Of S_PENDAFTARAN_H)
        'End Function
        'Public Function GetDataPendaftaranByKDREG(ByVal sParameter As String) As S_PENDAFTARAN_H
        '    If Not oConnection.GetConnection Then
        '        GetDataPendaftaranByKDREG = Nothing
        '        Exit Function
        '    End If
        '    GetDataPendaftaranByKDREG = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDREG = sParameter And x.SEP = "")
        'End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_UMPANBALIKs.FirstOrDefault(Function(x) x.SEP = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_UMPANBALIK) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_UMPANBALIKs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData("S_UMPANBALIK", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData("S_UMPANBALIK", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try
                InsertData = True
            Catch ex As Exception
                InsertData = False
                'oError.InsertData("S_UMPANBALIK", "INSERTDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_UMPANBALIK) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_UMPANBALIKs.FirstOrDefault(Function(x) x.SEP = entity.SEP)

                Try
                    'If ds.ISUMPANBALIK = False Then
                    '    oConnection.db.S_UMPANBALIKs.DeleteOnSubmit(ds)
                    '    oConnection.db.S_UMPANBALIKs.InsertOnSubmit(entity)
                    'End If

                    oConnection.db.S_UMPANBALIKs.DeleteOnSubmit(ds)
                    oConnection.db.S_UMPANBALIKs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData("MERGE", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData("MERGE", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData("MERGE", "UPDATEDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        'Public Function UpdateCheckedsep(ByVal sSEP As String, ByVal sDATEVERIFIKASI As DateTime, ByVal sBIAYA_RIILRS As Decimal, ByVal sBIAYA_DIAJUKAN As Decimal, ByVal sBIAYA_DISTEUJUI As Decimal) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateCheckedsep = False
        '            Exit Function
        '        End If

        '        Try
        '            Dim ds = oConnection.db.S_UMPANBALIKs.FirstOrDefault(Function(x) x.SEP = sSEP)

        '            If ds IsNot Nothing Then
        '                ds.DATEUPDATED = Now
        '                ds.ISUMPANBALIK = True
        '                ds.TANGGALVERIFIKASI = sDATEVERIFIKASI
        '                ds.BIAYA_DIAJUKAN = sBIAYA_DIAJUKAN
        '                ds.BIAYA_DISETUJUI = sBIAYA_DISTEUJUI
        '                ds.BIAYA_RIILRS = sBIAYA_RIILRS

        '                oConnection.db.SubmitChanges()
        '            End If

        '        Catch ex As Exception
        '            'oError.InsertData("SEP", "UPDATECHECKED", ex.ToString)
        '            Throw ex
        '        End Try

        '        UpdateCheckedsep = True
        '    Catch ex As Exception
        '        'UpdateCheckedsep = False
        '        oError.InsertData("SEP", "UPDATECHECKED", ex.ToString)
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace