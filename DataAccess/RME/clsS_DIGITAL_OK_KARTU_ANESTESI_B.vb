Imports DataAccess.My.Resources

Namespace EMedrek
    Public Class clsS_DIGITAL_OK_KARTU_ANESTESI_B
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public sTableName As String = "S_DIGITAL_OK_KARTU_ANESTESI_B"

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "KANASTESIB"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_KARTU_ANESTESI_B
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_KARTU_ANESTESI_B
        End Function
        Public Function GetData(ByVal sKODE As String) As S_DIGITAL_OK_KARTU_ANESTESI_B
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KODE = sKODE)
        End Function
        Public Function GetDataByKdpendaftaran(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_OK_KARTU_ANESTESI_B
            If Not oConnection.GetConnectionRME() Then
                GetDataByKdpendaftaran = Nothing
                Exit Function
            End If
            GetDataByKdpendaftaran = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataList() As List(Of S_DIGITAL_OK_KARTU_ANESTESI_B)
            If Not oConnection.GetConnectionRME Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRMList(ByVal RM As String) As List(Of S_DIGITAL_OK_KARTU_ANESTESI_B)
            If Not oConnection.GetConnectionRME Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.Where(Function(x) x.KDCUSTOMER = RM And x.ISDELETE = False).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function

        Public Function IsExist(ByVal sKDPENDAFTARAN As String) As Boolean
            If Not oConnection.GetConnectionRME Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function

        Public Function InsertData(ByVal entity As S_DIGITAL_OK_KARTU_ANESTESI_B) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERTDATA"


                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KODE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_KARTU_ANESTESI_B) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KODE = entity.KODE)

                Try
                    oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function UpdateDelete(ByVal sKODE As String, ByVal sUSER As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnectionRME Then
        '            UpdateDelete = False
        '            Exit Function
        '        End If

        '        sREFERENCE = sKODE

        '        Try
        '            Dim ds = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KODE = sKODE)

        '            ds.ISDELETE = 0
        '            ds.DATEDELETE = Now
        '            ds.USERDELETE = sUSER

        '            oConnection.dbRME.SubmitChanges()

        '        Catch ex As Exception
        '            oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", "UPDATEDELETE", ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        UpdateDelete = True
        '    Catch ex As Exception
        '        UpdateDelete = False
        '        oError.InsertData("S_DIGITAL_OK_KARTU_ANESTESI_B", "UPDATEDELETE", ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function

        Public Function DeleteData(ByVal Parameter As String, ByVal sUser As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.dbRME.S_DIGITAL_OK_KARTU_ANESTESI_Bs.FirstOrDefault(Function(x) x.KODE = Parameter)
                Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEUPDATED = WaktuServer
                    ds.ISDELETE = True
                    ds.DATEDELETE = WaktuServer
                    ds.USERDELETE = sUser

                    oConnection.dbRME.SubmitChanges()

                End If

            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace