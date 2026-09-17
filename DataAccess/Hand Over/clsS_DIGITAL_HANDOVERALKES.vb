Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_HANDOVERALKEALKES
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

            sMODUL = "HANDOVER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_HANDOVERALKE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_HANDOVERALKE
        End Function
        Public Function GetStructureHeaderList() As List(Of S_DIGITAL_HANDOVERALKE)
            If Not oConnection.GetConnection Then
                GetStructureHeaderList = Nothing
            End If
            GetStructureHeaderList = New List(Of S_DIGITAL_HANDOVERALKE)
        End Function

        Public Function GetData(ByVal sID As String) As S_DIGITAL_HANDOVERALKE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_HANDOVERALKEs.FirstOrDefault(Function(x) x.ID = sID)
        End Function
        Public Function GetDatabyKDREG(ByVal sParameter As String) As List(Of S_DIGITAL_HANDOVERALKE)
            If Not oConnection.GetConnection Then
                GetDatabyKDREG = Nothing
                Exit Function
            End If
            GetDatabyKDREG = oConnection.db.S_DIGITAL_HANDOVERALKEs.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).OrderBy(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataKDREG(ByVal Parameter1 As String) As S_DIGITAL_HANDOVERALKE
            If Not oConnection.GetConnection Then
                GetDataKDREG = Nothing
                Exit Function
            End If
            GetDataKDREG = oConnection.db.S_DIGITAL_HANDOVERALKEs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter1)
        End Function

        Public Function InsertDataList(ByVal entityDetail As List(Of S_DIGITAL_HANDOVERALKE)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertDataList = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_HANDOVERALKEs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_HANDOVERALKE", "INSERTDATA", ex.ToString,"")
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_HANDOVERALKE", "INSERTDATA", ex.ToString,"")
                    Throw ex
                End Try

                InsertDataList = True
            Catch ex As Exception
                InsertDataList = False
                oError.InsertData("S_DIGITAL_RI_29", "INSERTDATA", ex.ToString,"")
                Throw ex
            End Try
        End Function

        Public Function UpdateData(ByVal entity As S_DIGITAL_HANDOVERALKE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.ID
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_HANDOVERALKEs.FirstOrDefault(Function(x) x.ID = entity.ID)

                Try
                    oConnection.db.S_DIGITAL_HANDOVERALKEs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_HANDOVERALKEs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
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
        
    End Class
End Namespace