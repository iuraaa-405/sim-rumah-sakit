Imports Newtonsoft.Json.Linq
Imports System.Text
Imports Newtonsoft.Json


Public Class FhirBundleGenerator

    ''' <summary>
    ''' Fungsi untuk membuat FHIR Bundle JSON dari data discharge summary
    ''' </summary>
    Public Function CreateDischargeSummaryBundle() As String
        ' Membuat objek Bundle utama
        Dim bundle As New JObject()

        ' Set properti Bundle
        bundle("resourceType") = "Bundle"
        bundle("id") = GenerateId("bundle", "0901R001", "0464R0120326V000001")
        bundle("type") = "document"

        ' Meta object
        bundle("meta") = New JObject()
        bundle("meta")("lastUpdated") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        ' Identifier object
        bundle("identifier") = New JObject()
        bundle("identifier")("system") = "sep"
        bundle("identifier")("value") = "0464R0120326V000001"

        ' Membuat array entry
        Dim entries As New JArray()

        ' ========== ENTRY 1: Composition ==========
        entries.Add(CreateCompositionResource())

        ' ========== ENTRY 2: Patient ==========
        entries.Add(CreatePatientResource())

        ' ========== ENTRY 3: Encounter ==========
        entries.Add(CreateEncounterResource())

        ' ========== ENTRY 4: Condition ==========
        entries.Add(CreateConditionResource())

        ' ========== ENTRY 5: Practitioner ==========
        entries.Add(CreatePractitionerResource())

        ' ========== ENTRY 6: Organization ==========
        entries.Add(CreateOrganizationResource())

        ' ========== ENTRY 7: MedicationRequest (Obat 1) ==========
        entries.Add(CreateMedicationRequest("Paracetamol 500mg", "3 kali sehari 1 tablet sesudah makan", 10))

        ' ========== ENTRY 8: MedicationRequest (Obat 2) ==========
        entries.Add(CreateMedicationRequest("Amoxicillin 500mg", "3 kali sehari 1 tablet dihabiskan", 15))

        ' ========== ENTRY 9: AllergyIntolerance ==========
        entries.Add(CreateAllergyIntoleranceResource())

        ' Assign entries array ke bundle
        bundle("entry") = entries

        ' Mengembalikan string JSON dengan format indented
        Return bundle.ToString(Newtonsoft.Json.Formatting.Indented)
    End Function

    ''' <summary>
    ''' Membuat resource Composition
    ''' </summary>
    Private Function CreateCompositionResource() As JObject
        Dim composition As New JObject()
        composition("resourceType") = "Composition"
        composition("id") = GenerateId("composition", "0901R001", "0464R0120326V000001")
        composition("status") = "final"

        ' Type coding dengan text
        composition("type") = New JObject()
        Dim typeCoding As New JArray()
        Dim typeCodeObj As New JObject()
        typeCodeObj("system") = "http://loinc.org"
        typeCodeObj("code") = "81218-0"
        typeCoding.Add(typeCodeObj)
        composition("type")("coding") = typeCoding
        composition("type")("text") = "Discharge Summary"

        ' Subject reference
        composition("subject") = New JObject()
        composition("subject")("reference") = "Patient/" & GenerateId("patient", "0901R001", "0464R0120326V000001")
        composition("subject")("display") = "SOESANTO"

        ' Encounter reference
        composition("encounter") = New JObject()
        composition("encounter")("reference") = "Encounter/" & GenerateId("encounter", "0901R001", "0464R0120326V000001")

        composition("date") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        ' Author array
        Dim authors As New JArray()
        Dim author As New JObject()
        author("reference") = "Practitioner/" & GenerateId("practitioner", "0901R001", "0464R0120326V000001")
        author("display") = "TIA TRICIA DEVI, DR"
        authors.Add(author)
        composition("author") = authors

        composition("title") = "Discharge Summary"
        composition("confidentiality") = "N"

        ' Sections
        composition("section") = CreateSections()

        Return composition
    End Function

    ''' <summary>
    ''' Membuat sections untuk Composition
    ''' </summary>
    Private Function CreateSections() As JObject
        Dim sections As New JObject()

        ' Section 0: Reason for admission
        sections("0") = New JObject() From {
            {"title", "Reason for admission"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "29299-5"}, {"display", "Reason for visit Narrative"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>Pasien datang dengan keluhan luka bakar</div>"}}},
            {"entry", New JArray()}
        }

        ' Section 1: Chief complaint
        sections("1") = New JObject() From {
            {"title", "Chief complaint"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "10154-3"}, {"display", "Chief complaint Narrative"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>Keluhan utama: Luka bakar pada tubuh</div>"}}},
            {"entry", New JArray()}
        }

        ' Section 2: Admission diagnosis
        sections("2") = New JObject() From {
            {"title", "Admission diagnosis"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "42347-5"}, {"display", "Admission diagnosis Narrative"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>LUKA BAKAR 52% TBSA</div>"}}},
            {"entry", New JArray() From {
                New JObject() From {{"reference", "urn:uuid:" & Guid.NewGuid().ToString()}}
            }}
        }

        ' Section 4: Medications on Discharge
        sections("4") = New JObject() From {
            {"title", "Medications on Discharge"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "75311-1"}, {"display", "Hospital discharge medications Narrative"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>Paracetamol 500mg, Amoxicillin 500mg</div>"}}},
            {"mode", "working"},
            {"entry", New JArray() From {
                New JObject() From {{"reference", "MedicationRequest/" & GenerateId("medication1", "0901R001", "0464R0120326V000001")}},
                New JObject() From {{"reference", "MedicationRequest/" & GenerateId("medication2", "0901R001", "0464R0120326V000001")}}
            }}
        }

        ' Section 5: Plan of care
        sections("5") = New JObject() From {
            {"title", "Plan of care"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "18776-5"}, {"display", "Plan of care"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>Kontrol ke poli bedah 1 minggu lagi, rawat luka, hindari infeksi</div>"}}},
            {"mode", "working"},
            {"entry", New JArray()}
        }

        ' Section 7: Known allergies
        sections("7") = New JObject() From {
            {"title", "Known allergies"},
            {"code", New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://loinc.org"}, {"code", "48765-2"}, {"display", "Allergies and adverse reactions"}}
                }}
            }},
            {"text", New JObject() From {{"status", "additional"}, {"div", "<div>Tidak ada alergi yang diketahui</div>"}}},
            {"entry", New JArray() From {
                New JObject() From {{"reference", "AllergyIntolerance/" & GenerateId("allergy", "0901R001", "0464R0120326V000001")}}
            }}
        }

        Return sections
    End Function

    ''' <summary>
    ''' Membuat resource Patient
    ''' </summary>
    Private Function CreatePatientResource() As JObject
        Dim patient As New JObject()
        patient("resourceType") = "Patient"
        patient("id") = GenerateId("patient", "0901R001", "0464R0120326V000001")

        ' Patient identifiers
        Dim identifiers As New JArray()

        ' Identifier 1 - MR
        Dim id1 As New JObject()
        id1("use") = "usual"
        id1("type") = New JObject()
        id1("type")("coding") = New JArray() From {
            New JObject() From {{"system", "http://hl7.org/fhir/v2/0203"}, {"code", "MR"}}
        }
        id1("value") = "000005"
        id1("assigner") = New JObject() From {{"display", "IGD RS DUSTIRA"}}
        identifiers.Add(id1)

        ' Identifier 2 - BPJS
        Dim id2 As New JObject()
        id2("use") = "official"
        id2("type") = New JObject()
        id2("type")("coding") = New JArray() From {
            New JObject() From {{"system", "http://hl7.org/fhir/v2/0203"}, {"code", "MB"}}
        }
        id2("value") = "0002076061241"
        id2("assigner") = New JObject() From {{"display", "BPJS KESEHATAN"}}
        identifiers.Add(id2)

        ' Identifier 3 - NIK
        Dim id3 As New JObject()
        id3("use") = "official"
        id3("type") = New JObject()
        id3("type")("coding") = New JArray() From {
            New JObject() From {{"system", "http://hl7.org/fhir/v2/0203"}, {"code", "NIK"}}
        }
        id3("value") = "3508191202500001"
        id3("assigner") = New JObject() From {{"display", "KEMENDAGRI"}}
        identifiers.Add(id3)

        patient("identifier") = identifiers
        patient("active") = True

        ' Patient name
        patient("name") = New JArray() From {
            New JObject() From {{"use", "official"}, {"text", "SOESANTO"}}
        }

        patient("gender") = "male"
        patient("birthDate") = "1950-02-12"

        ' Marital status
        patient("maritalStatus") = New JObject()
        patient("maritalStatus")("coding") = New JArray() From {
            New JObject() From {{"system", "http://hl7.org/fhir/v3/MaritalStatus"}, {"code", "M"}, {"display", "Married"}}
        }

        ' Address
        patient("address") = New JArray() From {
            New JObject() From {
                {"line", New JArray() From {"JL JAYA NO. 119 RT 10000 RW 003"}},
                {"use", "home"},
                {"type", "both"},
                {"text", "JL JAYA NO. 119 RT 10000 RW 003"}
            }
        }

        ' Telecom
        patient("telecom") = New JArray() From {
            New JObject() From {{"system", "phone"}, {"value", "08123456789"}, {"use", "mobile"}}
        }

        ' Managing organization
        patient("managingOrganization") = New JObject()
        patient("managingOrganization")("reference") = "Organization/" & GenerateId("organization", "0901R001", "0464R0120326V000001")
        patient("managingOrganization")("display") = "IGD RS DUSTIRA"

        Return patient
    End Function

    ''' <summary>
    ''' Membuat resource Encounter
    ''' </summary>
    Private Function CreateEncounterResource() As JObject
        Dim encounter As New JObject()
        encounter("resourceType") = "Encounter"
        encounter("id") = GenerateId("encounter", "0901R001", "0464R0120326V000001")

        ' Identifier
        encounter("identifier") = New JArray() From {
            New JObject() From {{"system", "id_encounter"}, {"value", "0464R0120326V000001"}},
            New JObject() From {{"system", "http://api.bpjs-kesehatan.go.id:8080/Vclaim-rest/SEP/"}, {"value", "0464R0120326V000001"}}
        }

        encounter("status") = "finished"

        ' Status history
        encounter("statusHistory") = New JArray() From {
            New JObject() From {
                {"status", "arrived"},
                {"period", New JObject() From {
                    {"start", "2026-03-20 08:00:00"},
                    {"end", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
                }}
            }
        }

        ' Class
        encounter("class") = New JObject()
        encounter("class")("system") = "http://hl7.org/fhir/v3/ActCode"
        encounter("class")("code") = "IMP"
        encounter("class")("display") = "inpatient encounter"

        ' Subject
        encounter("subject") = New JObject()
        encounter("subject")("reference") = "Patient/" & GenerateId("patient", "0901R001", "0464R0120326V000001")
        encounter("subject")("display") = "SOESANTO"

        ' Participant
        encounter("participant") = New JArray() From {
            New JObject() From {
                {"individual", New JObject() From {
                    {"reference", "Practitioner/" & GenerateId("practitioner", "0901R001", "0464R0120326V000001")},
                    {"display", "TIA TRICIA DEVI, DR"}
                }}
            }
        }

        ' Period
        encounter("period") = New JObject()
        encounter("period")("start") = "2026-03-20 08:00:00"
        encounter("period")("end") = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        ' Reason
        encounter("reason") = New JArray() From {
            New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://hl7.org/fhir/sid/icd-10"}, {"code", "T31.4"}, {"display", "Burns involving 40-49% of body surface"}}
                }},
                {"text", "Rawat Inap"}
            }
        }

        ' Diagnosis
        encounter("diagnosis") = New JArray() From {
            New JObject() From {
                {"condition", New JObject() From {
                    {"reference", "Condition/" & GenerateId("condition", "0901R001", "0464R0120326V000001")},
                    {"display", "Burns involving 40-49% of body surface"}
                }},
                {"rank", 1}
            }
        }

        ' Hospitalization
        encounter("hospitalization") = New JObject()
        encounter("hospitalization")("dischargeDisposition") = New JArray() From {
            New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://hl7.org/fhir/discharge-disposition"}, {"code", "home"}, {"display", "Home"}}
                }}
            }
        }

        ' Service provider
        encounter("serviceProvider") = New JObject()
        encounter("serviceProvider")("reference") = "Organization/" & GenerateId("organization", "0901R001", "0464R0120326V000001")
        encounter("serviceProvider")("display") = "IGD RS DUSTIRA"

        Return encounter
    End Function

    ''' <summary>
    ''' Membuat resource Condition (Diagnosis)
    ''' </summary>
    Private Function CreateConditionResource() As JObject
        Dim condition As New JObject()
        condition("resourceType") = "Condition"
        condition("id") = GenerateId("condition", "0901R001", "0464R0120326V000001")
        condition("clinicalStatus") = "active"
        condition("verificationStatus") = "confirmed"

        ' Category
        condition("category") = New JArray() From {
            New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://hl7.org/fhir/condition-category"}, {"code", "encounter-diagnosis"}, {"display", "Encounter Diagnosis"}}
                }}
            }
        }

        ' Code
        condition("code") = New JObject()
        condition("code")("coding") = New JArray() From {
            New JObject() From {{"system", "http://hl7.org/fhir/sid/icd-10"}, {"code", "T31.4"}, {"display", "Burns involving 40-49% of body surface"}}
        }
        condition("code")("text") = "Burns involving 40-49% of body surface"

        ' Subject
        condition("subject") = New JObject()
        condition("subject")("reference") = "Patient/" & GenerateId("patient", "0901R001", "0464R0120326V000001")

        condition("onsetDateTime") = "2026-03-20 08:00:00"

        Return condition
    End Function

    ''' <summary>
    ''' Membuat resource Practitioner
    ''' </summary>
    Private Function CreatePractitionerResource() As JObject
        Dim practitioner As New JObject()
        practitioner("resourceType") = "Practitioner"
        practitioner("id") = GenerateId("practitioner", "0901R001", "0464R0120326V000001")

        ' Identifier
        practitioner("identifier") = New JArray() From {
            New JObject() From {
                {"use", "official"},
                {"system", "urn:oid:nomor_sip"},
                {"value", "1.2.01.3173.1834/14022/04.16.1"}
            },
            New JObject() From {
                {"use", "official"},
                {"type", New JObject() From {
                    {"coding", New JArray() From {
                        New JObject() From {{"system", "http://hl7.org/fhir/v2/0203"}, {"code", "NNIDN"}}
                    }}
                }},
                {"value", "3172055103530001"},
                {"assigner", New JObject() From {{"display", "KEMENDAGRI"}}}
            }
        }

        ' Name
        practitioner("name") = New JArray() From {
            New JObject() From {{"use", "official"}, {"text", "TIA TRICIA DEVI, DR"}}
        }

        ' Gender
        practitioner("gender") = "female"

        ' Birth date
        practitioner("birthDate") = "1975-03-15"

        ' Address
        practitioner("address") = New JArray() From {
            New JObject() From {
                {"use", "home"},
                {"line", New JArray() From {"Jl. Praktik No. 123"}},
                {"city", "Jakarta"},
                {"postalCode", "11410"},
                {"country", "ID"}
            }
        }

        Return practitioner
    End Function

    ''' <summary>
    ''' Membuat resource Organization
    ''' </summary>
    Private Function CreateOrganizationResource() As JObject
        Dim organization As New JObject()
        organization("resourceType") = "Organization"
        organization("id") = GenerateId("organization", "0901R001", "0464R0120326V000001")

        ' Identifier
        organization("identifier") = New JArray() From {
            New JObject() From {{"use", "official"}, {"system", "urn:oid:bpjs"}, {"value", "0901R001"}},
            New JObject() From {{"use", "official"}, {"system", "urn:oid:kemkes"}, {"value", "3173014"}}
        }

        ' Type
        organization("type") = New JArray() From {
            New JObject() From {
                {"coding", New JArray() From {
                    New JObject() From {{"system", "http://hl7.org/fhir/organization-type"}, {"code", "prov"}, {"display", "Healthcare Provider"}}
                }}
            }
        }

        organization("name") = "IGD RS DUSTIRA"
        organization("alias") = New JArray() From {"IGD RS DUSTIRA"}

        ' Telecom
        organization("telecom") = New JArray() From {
            New JObject() From {{"system", "phone"}, {"value", "1500-135"}, {"use", "work"}}
        }

        ' Address
        organization("address") = New JArray() From {
            New JObject() From {{"use", "work"}, {"country", "IDN"}}
        }

        Return organization
    End Function

    ''' <summary>
    ''' Membuat resource MedicationRequest
    ''' </summary>
    Private Function CreateMedicationRequest(medicationName As String, dosageText As String, quantityValue As Integer) As JObject
        Dim medIdSuffix As String = If(medicationName.Contains("Paracetamol"), "medication1", "medication2")

        Dim medRequest As New JObject()
        medRequest("resourceType") = "MedicationRequest"
        medRequest("id") = GenerateId(medIdSuffix, "0901R001", "0464R0120326V000001")
        medRequest("status") = "active"
        medRequest("intent") = "order"

        ' Medication codeable concept
        medRequest("medicationCodeableConcept") = New JObject()
        medRequest("medicationCodeableConcept")("coding") = New JArray() From {
            New JObject() From {{"system", "http://sys-pharmacy.com/code"}, {"display", medicationName}}
        }
        medRequest("medicationCodeableConcept")("text") = medicationName

        ' Subject
        medRequest("subject") = New JObject()
        medRequest("subject")("reference") = "Patient/" & GenerateId("patient", "0901R001", "0464R0120326V000001")
        medRequest("subject")("display") = "SOESANTO"

        ' Encounter
        medRequest("encounter") = New JObject()
        medRequest("encounter")("reference") = "Encounter/" & GenerateId("encounter", "0901R001", "0464R0120326V000001")

        ' Authored on
        medRequest("authoredOn") = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss+07:00")

        ' Requester
        medRequest("requester") = New JObject()
        medRequest("requester")("reference") = "Practitioner/" & GenerateId("practitioner", "0901R001", "0464R0120326V000001")
        medRequest("requester")("display") = "TIA TRICIA DEVI, DR"

        ' Dosage instruction
        medRequest("dosageInstruction") = New JArray() From {
            New JObject() From {{"text", dosageText}}
        }

        ' Dispense request
        medRequest("dispenseRequest") = New JObject()
        medRequest("dispenseRequest")("quantity") = New JObject()
        medRequest("dispenseRequest")("quantity")("value") = quantityValue
        medRequest("dispenseRequest")("quantity")("unit") = "TAB"

        Return medRequest
    End Function

    ''' <summary>
    ''' Membuat resource AllergyIntolerance
    ''' </summary>
    Private Function CreateAllergyIntoleranceResource() As JObject
        Dim allergy As New JObject()
        allergy("resourceType") = "AllergyIntolerance"
        allergy("id") = GenerateId("allergy", "0901R001", "0464R0120326V000001")
        allergy("clinicalStatus") = "active"
        allergy("verificationStatus") = "confirmed"
        allergy("type") = "allergy"
        allergy("category") = New JArray() From {"medication"}
        allergy("criticality") = "low"

        ' Code
        allergy("code") = New JObject()
        allergy("code")("coding") = New JArray() From {
            New JObject() From {{"system", "http://snomed.info/sct"}, {"code", "716186003"}, {"display", "No known allergy"}}
        }
        allergy("code")("text") = "No known allergy"

        ' Patient
        allergy("patient") = New JObject()
        allergy("patient")("reference") = "Patient/" & GenerateId("patient", "0901R001", "0464R0120326V000001")

        ' Recorded date
        allergy("recordedDate") = DateTime.Now.ToString("yyyy-MM-dd")

        Return allergy
    End Function

    ''' <summary>
    ''' Fungsi helper untuk generate ID yang konsisten
    ''' </summary>
    Private Function GenerateId(prefix As String, kodeRS As String, noSep As String) As String
        Return $"{prefix}-{kodeRS}-{noSep}"
    End Function

#Region "Json"
    Public Shared Function JSONEncounterKunjunganBaru(snowmed As String, snowmedDisplay As String, orgId As String, registrationId As String, patientId As String, patientName As String, practitionerId As String, practitionerName As String, locationPoli As String, locationPoliName As String, startTime As String) As String
        '"                ""system"": ""http://terminology.hl7.org/CodeSystem/service-type""," & vbCrLf &

        Dim json As String = ""

        json = "{" & vbCrLf &
               "    ""resourceType"": ""Encounter""," & vbCrLf &
               "    ""identifier"": [" & vbCrLf &
               "        {" & vbCrLf &
               "            ""system"": ""http://sys-ids.kemkes.go.id/encounter/" & orgId & """," & vbCrLf &
               "            ""value"": """ & registrationId & """" & vbCrLf &
               "        }" & vbCrLf &
               "    ]," & vbCrLf &
               "    ""status"": ""arrived""," & vbCrLf &
               "    ""class"": {" & vbCrLf &
               "        ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ActCode""," & vbCrLf &
               "        ""code"": ""AMB""," & vbCrLf &
               "        ""display"": ""ambulatory""" & vbCrLf &
               "    }," & vbCrLf &
               "    ""serviceType"": {" & vbCrLf &
               "        ""coding"": [" & vbCrLf &
               "            {" & vbCrLf &
               "                ""system"": ""http://terminology.hl7.org/CodeSystem/service-type""," & vbCrLf &
               "                ""code"": """ & snowmed & """," & vbCrLf &
               "                ""display"": """ & snowmedDisplay & """" & vbCrLf &
               "            }" & vbCrLf &
               "        ]" & vbCrLf &
               "    }," & vbCrLf &
               "    ""subject"": {" & vbCrLf &
               "        ""reference"": ""Patient/" & patientId & """," & vbCrLf &
               "        ""display"": """ & patientName & """" & vbCrLf &
               "    }," & vbCrLf &
               "    ""participant"": [" & vbCrLf &
               "        {" & vbCrLf &
               "            ""type"": [" & vbCrLf &
               "                {" & vbCrLf &
               "                    ""coding"": [" & vbCrLf &
               "                        {" & vbCrLf &
               "                            ""system"": ""http://terminology.hl7.org/CodeSystem/v3-ParticipationType""," & vbCrLf &
               "                            ""code"": ""ATND""," & vbCrLf &
               "                            ""display"": ""attender""" & vbCrLf &
               "                        }" & vbCrLf &
               "                    ]" & vbCrLf &
               "                }" & vbCrLf &
               "            ]," & vbCrLf &
               "            ""individual"": {" & vbCrLf &
               "                ""reference"": ""Practitioner/" & practitionerId & """," & vbCrLf &
               "                ""display"": """ & practitionerName & """" & vbCrLf &
               "            }" & vbCrLf &
               "        }" & vbCrLf &
               "    ]," & vbCrLf &
               "    ""period"": {" & vbCrLf &
               "        ""start"": """ & startTime & """" & vbCrLf &
               "    }," & vbCrLf &
               "    ""location"": [" & vbCrLf &
               "        {" & vbCrLf &
               "            ""location"": {" & vbCrLf &
               "                ""reference"": ""Location/" & locationPoli & """," & vbCrLf &
               "                ""display"": """ & locationPoliName & """" & vbCrLf &
               "            }," & vbCrLf &
               "            ""period"": {" & vbCrLf &
               "                ""start"": """ & startTime & """" & vbCrLf &
               "            }," & vbCrLf &
               "            ""extension"": [" & vbCrLf &
               "                {" & vbCrLf &
               "                    ""url"": ""https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass""," & vbCrLf &
               "                    ""extension"": [" & vbCrLf &
               "                        {" & vbCrLf &
               "                            ""url"": ""value""," & vbCrLf &
               "                            ""valueCodeableConcept"": {" & vbCrLf &
               "                                ""coding"": [" & vbCrLf &
               "                                    {" & vbCrLf &
               "                                        ""system"": ""http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient""," & vbCrLf &
               "                                        ""code"": ""reguler""," & vbCrLf &
               "                                        ""display"": ""Kelas Reguler""" & vbCrLf &
               "                                    }" & vbCrLf &
               "                                ]" & vbCrLf &
               "                            }" & vbCrLf &
               "                        }," & vbCrLf &
               "                        {" & vbCrLf &
               "                            ""url"": ""upgradeClassIndicator""," & vbCrLf &
               "                            ""valueCodeableConcept"": {" & vbCrLf &
               "                                ""coding"": [" & vbCrLf &
               "                                    {" & vbCrLf &
               "                                        ""system"": ""http://terminology.kemkes.go.id/CodeSystem/locationUpgradeClass""," & vbCrLf &
               "                                        ""code"": ""kelas-tetap""," & vbCrLf &
               "                                        ""display"": ""Kelas Tetap Perawatan""" & vbCrLf &
               "                                    }" & vbCrLf &
               "                                ]" & vbCrLf &
               "                            }" & vbCrLf &
               "                        }" & vbCrLf &
               "                    ]" & vbCrLf &
               "                }" & vbCrLf &
               "            ]" & vbCrLf &
               "        }" & vbCrLf &
               "    ]," & vbCrLf &
               "    ""statusHistory"": [" & vbCrLf &
               "        {" & vbCrLf &
               "            ""status"": ""arrived""," & vbCrLf &
               "            ""period"": {" & vbCrLf &
               "                ""start"": """ & startTime & """" & vbCrLf &
               "            }" & vbCrLf &
               "        }" & vbCrLf &
               "    ]," & vbCrLf &
               "    ""serviceProvider"": {" & vbCrLf &
               "        ""reference"": ""Organization/" & orgId & """" & vbCrLf &
               "    }" & vbCrLf &
               "}"

        json = System.Text.RegularExpressions.Regex.Replace(json, "\s+", " ")

        Return json
    End Function
    Public Shared Function JSONEncounterMasukRuang(id As String, snowmed As String, snowmedDisplay As String, orgId As String, registrationId As String, patientId As String, patientName As String, practitionerId As String, practitionerName As String, locationPoli As String, locationPoliName As String, startTime_taskid3 As String, endarrived_taskid4 As String) As String
        Dim encounterData = New With {
        .resourceType = "Encounter",
        .id = "" & id & "",
        .identifier = New Object() {
            New With {
                .system = "http://sys-ids.kemkes.go.id/encounter/" & orgId & "",
                .value = "" & registrationId & ""
            }
        },
        .status = "in-progress",
        .class = New With {
            .system = "http://terminology.hl7.org/CodeSystem/v3-ActCode",
            .code = "AMB",
            .display = "ambulatory"
        },
        .serviceType = New With {
            .coding = New Object() {
                New With {
                    .system = "http://terminology.hl7.org/CodeSystem/service-type",
                    .code = "" & snowmed & "",
                    .display = "" & snowmedDisplay & ""
                }
            }
        },
        .subject = New With {
            .reference = "Patient/" & patientId & "",
            .display = "" & patientName & ""
        },
        .participant = New Object() {
            New With {
                .type = New Object() {
                    New With {
                        .coding = New Object() {
                            New With {
                                .system = "http://terminology.hl7.org/CodeSystem/v3-ParticipationType",
                                .code = "ATND",
                                .display = "attender"
                            }
                        }
                    }
                },
                .individual = New With {
                    .reference = "Practitioner/" & practitionerId & "",
                    .display = "" & practitionerName & ""
                }
            }
        },
        .period = New With {
            .start = "" & startTime_taskid3 & ""
        },
        .location = New Object() {
            New With {
                .location = New With {
                    .reference = "Location/" & locationPoli & "",
                    .display = "" & locationPoliName & ""
                },
                .period = New With {
                    .start = "" & startTime_taskid3 & ""
                },
                .extension = New Object() {
                    New With {
                        .url = "https://fhir.kemkes.go.id/r4/StructureDefinition/ServiceClass",
                        .extension = New Object() {
                            New With {
                                .url = "value",
                                .valueCodeableConcept = New With {
                                    .coding = New Object() {
                                        New With {
                                            .system = "http://terminology.kemkes.go.id/CodeSystem/locationServiceClass-Outpatient",
                                            .code = "reguler",
                                            .display = "Kelas Reguler"
                                        }
                                    }
                                }
                            },
                            New With {
                                .url = "upgradeClassIndicator",
                                .valueCodeableConcept = New With {
                                    .coding = New Object() {
                                        New With {
                                            .system = "http://terminology.kemkes.go.id/CodeSystem/locationUpgradeClass",
                                            .code = "kelas-tetap",
                                            .display = "Kelas Tetap Perawatan"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        },
        .statusHistory = New Object() {
            New With {
                .status = "arrived",
                .period = New With {
                    .start = "" & startTime_taskid3 & "",
                    .[end] = "" & endarrived_taskid4 & ""
                }
            },
            New With {
                .status = "in-progress",
                .period = New With {
                    .start = "" & endarrived_taskid4 & ""
                }
            }
        },
        .serviceProvider = New With {
            .reference = "Organization/" & orgId & ""
        }
    }


        Return JsonConvert.SerializeObject(encounterData, Formatting.Indented)


    End Function
    Public Shared Function CreateConditionJsonKeluhanUtama(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As String) As String

        Dim condition = New With {
            .resourceType = "Condition",
            .clinicalStatus = New With {
                .coding = New Object() {
                    New With {
                        .system = "http://terminology.hl7.org/CodeSystem/condition-clinical",
                        .code = "active",
                        .display = "Active"
                    }
                }
            },
            .category = New Object() {
                New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://terminology.kemkes.go.id",
                            .code = "chief-complaint",
                            .display = "Chief Complaint"
                        }
                    }
                }
            },
            .code = New With {
                .coding = New Object() {
                    New With {
                        .system = "http://hl7.org/fhir/sid/icd-10",
                        .code = "" & snowmed & "",
                        .display = "" & snowmedDisplay & ""
                    }
                }
            },
            .subject = New With {
                .reference = $"Patient/{patientId}",
                .display = patientName
            },
            .encounter = New With {
                .reference = $"Encounter/{encounterId}"
            },
            .onsetDateTime = "" & onsetDateTime & "",
            .recordedDate = "" & recordedDate & "",
            .recorder = New With {
                .reference = $"Practitioner/{practitionerId}",
                .display = practitionerName
            },
            .note = New Object() {
                New With {
                    .text = "" & kondisi & ""
                }
            }
        }

        ' Serialize with indentation
        Dim jsonString As String = JsonConvert.SerializeObject(condition, Formatting.Indented)

        Return jsonString
    End Function
    Public Shared Function CreateConditionJsonKeluhanPenyerta(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String) As String

        Try
            Dim condition As New JObject(
                New JProperty("resourceType", "Condition"),
                New JProperty("clinicalStatus", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://terminology.hl7.org/CodeSystem/condition-clinical"),
                            New JProperty("code", "active"),
                            New JProperty("display", "Active")
                        )
                    ))
                )),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/condition-category"),
                                New JProperty("code", "problem-list-item"),
                                New JProperty("display", "Problem List Item")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://hl7.org/fhir/sid/icd-10"),
                            New JProperty("code", "" & snowmed & ""),
                            New JProperty("display", "" & snowmedDisplay & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("onsetDateTime", onsetDateTime),
                New JProperty("recordedDate", recordedDate),
                New JProperty("recorder", New JObject(
                    New JProperty("reference", $"Practitioner/{practitionerId}"),
                    New JProperty("display", practitionerName)
                ))
            )

            Return condition.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating condition JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonSistol(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "8480-6" & ""),
                            New JProperty("display", "" & "Systolic blood pressure" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "mm[Hg]"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "mm[Hg]")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonDiastol(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "8462-4" & ""),
                            New JProperty("display", "" & "Diastolic blood pressure" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "mm[Hg]"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "mm[Hg]")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonSuhuTubuh(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "8310-5" & ""),
                            New JProperty("display", "" & "Body temperature" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "Cel"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "Cel")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonDenyutJantung(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "8867-4" & ""),
                            New JProperty("display", "" & "Heart rate" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "{beats}/min"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "{beats}/min")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonPernapasan(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "9279-1" & ""),
                            New JProperty("display", "" & "Respiratory rate" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "breaths/min"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "/min")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonTingkatKesadaran(patientId As String, patientName As String,
                                              encounterId As String, practitionerId As String,
                                              practitionerName As String, snowmedtingkatkesadaran As String, snowmedDisplaytingkatkesadaran As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Dim observation = New With {
            .resourceType = "Observation",
            .status = "final",
            .category = New Object() {
                New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://terminology.hl7.org/CodeSystem/observation-category",
                            .code = "vital-signs",
                            .display = "Vital Signs"
                        }
                    }
                }
            },
            .code = New With {
                .coding = New Object() {
                    New With {
                        .system = "http://loinc.org",
                        .code = "67775-7",
                        .display = "Level of responsiveness"
                    }
                }
            },
            .subject = New With {
                .reference = "Patient/" & patientId & "",
                .display = "" & patientName & ""
            },
            .encounter = New With {
                .reference = "Encounter/" & encounterId & ""
            },
            .effectiveDateTime = "" & onsetDateTime & "",
            .issued = "" & recordedDate & "",
            .performer = New Object() {
                New With {
                    .reference = "Practitioner/" & practitionerId & "",
                    .display = "" & practitionerName & ""
                }
            },
            .valueCodeableConcept = New With {
                .coding = New Object() {
                    New With {
                        .system = "http://snomed.info/sct",
                        .code = "" & snowmedtingkatkesadaran & "",
                        .display = "" & snowmedDisplaytingkatkesadaran & " alert"
                    }
                }
            }
        }

        Return JsonConvert.SerializeObject(observation, Formatting.Indented)
    End Function
    Public Shared Function CreateObservationJsonTinggiBadan(patientId As String, patientName As String,
                                                  encounterId As String, practitionerId As String,
                                                  practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "8302-2" & ""),
                            New JProperty("display", "" & "Body height" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "cm"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "cm")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function CreateObservationJsonBeratBadan(patientId As String, patientName As String,
                                                  encounterId As String, practitionerId As String,
                                                  practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As Integer) As String

        Try
            Dim observation As New JObject(
                New JProperty("resourceType", "Observation"),
                New JProperty("status", "final"),
                New JProperty("category", New JArray(
                    New JObject(
                        New JProperty("coding", New JArray(
                            New JObject(
                                New JProperty("system", "http://terminology.hl7.org/CodeSystem/observation-category"),
                                New JProperty("code", "vital-signs"),
                                New JProperty("display", "Vital Signs")
                            )
                        ))
                    )
                )),
                New JProperty("code", New JObject(
                    New JProperty("coding", New JArray(
                        New JObject(
                            New JProperty("system", "http://loinc.org"),
                            New JProperty("code", "" & "29463-7" & ""),
                            New JProperty("display", "" & "Body weight" & "")
                        )
                    ))
                )),
                New JProperty("subject", New JObject(
                    New JProperty("reference", $"Patient/{patientId}"),
                    New JProperty("display", patientName)
                )),
                New JProperty("encounter", New JObject(
                    New JProperty("reference", $"Encounter/{encounterId}")
                )),
                New JProperty("effectiveDateTime", onsetDateTime),
                New JProperty("issued", recordedDate),
                New JProperty("performer", New JArray(
                    New JObject(
                        New JProperty("reference", $"Practitioner/{practitionerId}"),
                        New JProperty("display", practitionerName)
                    )
                )),
                New JProperty("valueQuantity", New JObject(
                    New JProperty("value", JToken.FromObject(kondisi)),
                    New JProperty("unit", "kg"),
                    New JProperty("system", "http://unitsofmeasure.org"),
                    New JProperty("code", "kg")
                ))
            )

            Return observation.ToString(Formatting.Indented)
        Catch ex As Exception
            Throw New Exception($"Error creating observation JSON: {ex.Message}")
        End Try
    End Function
    Public Shared Function BuildClinicalImpressionJsonAnonymous(patientId As String, patientName As String,
                                                  encounterId As String, practitionerId As String,
                                                  practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As String) As String

        Dim clinicalImpression = New With {
            .resourceType = "ClinicalImpression",
            .status = "completed",
            .code = New With {
                .coding = New Object() {
                    New With {
                        .system = "http://snomed.info/sct",
                        .code = "312850006",
                        .display = "History of disorder"
                    }
                }
            },
            .subject = New With {
                .reference = "Patient/" & patientId & "",
                .display = "" & patientName & ""
            },
            .encounter = New With {
                .reference = "Encounter/" & encounterId & ""
            },
            .effectiveDateTime = "" & onsetDateTime & "",
            .date = "" & recordedDate & "",
            .assessor = New With {
                .reference = "Practitioner/" & practitionerId & ""
            },
            .summary = "" & kondisi & ""
        }

        Return JsonConvert.SerializeObject(clinicalImpression, Formatting.Indented)
    End Function
    Public Function BuildGoalJsonAnonymous(patientId As String, patientName As String,
                                                  encounterId As String, practitionerId As String,
                                                  practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As String) As String

        Dim goal = New With {
        .resourceType = "Goal",
        .lifecycleStatus = "planned",
        .category = New Object() {
            New With {
                .coding = New Object() {
                    New With {
                        .system = "http://terminology.hl7.org/CodeSystem/goal-category",
                        .code = "nursing",
                        .display = "Nursing"
                    }
                }
            }
        },
        .description = New With {
            .text = "" & kondisi & ""
        },
        .subject = New With {
            .reference = "Patient/{{Patient_id}}"
        },
        .target = New Object() {
            New With {
                .measure = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://loinc.org",
                            .code = "8480-6",
                            .display = "Systolic blood pressure"
                        }
                    }
                },
                .detailCodeableConcept = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://snomed.info/sct",
                            .code = "17621005",
                            .display = "Normal"
                        }
                    }
                },
                .dueDate = "2023-06-04"
            },
            New With {
                .measure = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://loinc.org",
                            .code = "8462-4",
                            .display = "Diastolic blood pressure"
                        }
                    }
                },
                .detailCodeableConcept = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://snomed.info/sct",
                            .code = "17621005",
                            .display = "Normal"
                        }
                    }
                },
                .dueDate = "2023-06-04"
            },
            New With {
                .measure = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://loinc.org",
                            .code = "26515-7",
                            .display = "Platelets [#/volume] in Blood"
                        }
                    }
                },
                .detailCodeableConcept = New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://snomed.info/sct",
                            .code = "17621005",
                            .display = "Normal"
                        }
                    }
                },
                .dueDate = "2023-06-04"
            }
        },
        .statusDate = "2023-06-04",
        .expressedBy = New With {
            .reference = "Practitioner/{{Practitioner_id}}"
        },
        .addresses = New Object() {
            New With {
                .reference = "Condition/{{Condition_KeluhanUtama}}"
            }
        }
    }

        Return JsonConvert.SerializeObject(goal, Formatting.Indented)
    End Function
    Public Shared Function BuildCarePlanJsonAnonymous(patientId As String, patientName As String,
                                                  encounterId As String, practitionerId As String,
                                                  practitionerName As String, snowmed As String, snowmedDisplay As String, onsetDateTime As String, recordedDate As String, kondisi As String, idgoal As String) As String

        Dim carePlan = New With {
            .resourceType = "CarePlan",
            .status = "active",
            .intent = "plan",
            .category = New Object() {
                New With {
                    .coding = New Object() {
                        New With {
                            .system = "http://snomed.info/sct",
                            .code = "736271009",
                            .display = "Outpatient care plan"
                        }
                    }
                }
            },
            .title = "Rencana Rawat Pasien",
            .description = "" & kondisi & "",
            .subject = New With {
                .reference = "Patient/" & patientId & "",
                .display = "" & patientName & ""
            },
            .encounter = New With {
                .reference = "Encounter/" & encounterId & ""
            },
            .created = "" & onsetDateTime & "",
            .author = New With {
                .reference = "Practitioner/" & practitionerId & "",
                .display = "" & practitionerName & ""
            },
            .goal = New Object() {
                New With {
                    .reference = "Goal/" & idgoal & ""
                }
            }
        }

        Return JsonConvert.SerializeObject(carePlan, Formatting.Indented)
    End Function
#End Region
End Class