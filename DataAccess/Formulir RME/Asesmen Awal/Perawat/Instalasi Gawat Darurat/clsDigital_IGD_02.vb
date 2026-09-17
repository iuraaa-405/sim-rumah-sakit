Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_IGD_02
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "AMPIGD"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_02_NEW
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_02_NEW
        End Function
        Public Function GetData(ByVal sKDASESMEN As String) As S_DIGITAL_IGD_02_NEW
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)
        End Function
        Public Function GetDataPendaftaran(ByVal sKDASESMEN As String) As S_DIGITAL_IGD_02_NEW
            If Not oConnection.GetConnectionRME() Then
                GetDataPendaftaran = Nothing
                Exit Function
            End If
            GetDataPendaftaran = oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDASESMEN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_02_NEW) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "INSERT"

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KDASESMEN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDASESMEN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_02_NEW) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.FirstOrDefault(Function(x) x.KDASESMEN = entity.KDASESMEN)

                Try
                    oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDelete(ByVal sKDASESMEN As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDelete = False
                    Exit Function
                End If

                sREFERENCE = sKDASESMEN

                Try
                    Dim ds = oConnection.dbRME.S_DIGITAL_IGD_02_NEWs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)

                    ds.ISDELETE = 0
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_IGD_02_NEW", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDelete = True
            Catch ex As Exception
                UpdateDelete = False
                oError.InsertData("S_DIGITAL_IGD_02_NEW", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace