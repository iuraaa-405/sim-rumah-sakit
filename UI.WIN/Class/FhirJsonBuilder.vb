Imports Newtonsoft.Json.Linq
Imports System
Imports System.Text

Public Class FhirJsonBuilder

    Public Function BuildBundleWithComposition(
    bundleId As String,
    sepNumber As String,
    patientId As String,
    patientName As String,
    encounterId As String,
    practitionerId As String,
    practitionerName As String,
    compositionId As String,
    admissionDiagnosisText As String,
    dischargeDiagnosisText As String,
    medicationsText As String,
    planOfCareText As String,
    allergiesText As String,
    conditionId As String,
    allergyId As String) As String

        Dim sb As New StringBuilder()

        ' =====================================================
        ' BUNDLE AWAL
        ' =====================================================
        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": {")

        ' =====================================================
        ' COMPOSITION RESOURCE
        ' =====================================================
        sb.AppendLine("        ""resourceType"": ""Composition"",")
        sb.AppendLine($"        ""id"": ""{compositionId}"",")
        sb.AppendLine("        ""status"": ""final"",")
        sb.AppendLine("        ""type"": {")
        sb.AppendLine("          ""coding"": [")
        sb.AppendLine("            {")
        sb.AppendLine("              ""system"": ""http://loinc.org"",")
        sb.AppendLine("              ""code"": ""81218-0""")
        sb.AppendLine("            }")
        sb.AppendLine("          ],")
        sb.AppendLine("          ""text"": ""Discharge Summary""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""subject"": {")
        sb.AppendLine($"          ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"          ""display"": ""{EscapeJson(patientName)}""")
        sb.AppendLine("        },")
        sb.AppendLine("        ""encounter"": {")
        sb.AppendLine($"          ""reference"": ""Encounter/{encounterId}""")
        sb.AppendLine("        },")
        sb.AppendLine($"        ""date"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}"",")
        sb.AppendLine("        ""author"": [")
        sb.AppendLine("          {")
        sb.AppendLine($"            ""reference"": ""Practitioner/{practitionerId}"",")
        sb.AppendLine($"            ""display"": ""{EscapeJson(practitionerName)}""")
        sb.AppendLine("          }")
        sb.AppendLine("        ],")
        sb.AppendLine("        ""title"": ""Discharge Summary"",")
        sb.AppendLine("        ""confidentiality"": ""N"",")
        sb.AppendLine("        ""section"": {")

        ' Section 0 - Reason for admission
        sb.AppendLine("          ""0"": {")
        sb.AppendLine("            ""title"": ""Reason for admission"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""29299-5"",")
        sb.AppendLine("                  ""display"": ""Reason for visit Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine("              ""div"": ""<div></div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 1 - Chief complaint
        sb.AppendLine("          ""1"": {")
        sb.AppendLine("            ""title"": ""Chief complaint"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""10154-3"",")
        sb.AppendLine("                  ""display"": ""Chief complaint Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine("              ""div"": ""<div></div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 2 - Admission diagnosis
        sb.AppendLine("          ""2"": {")
        sb.AppendLine("            ""title"": ""Admission diagnosis"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""42347-5"",")
        sb.AppendLine("                  ""display"": ""Admission diagnosis Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(admissionDiagnosisText)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": [")
        sb.AppendLine("              {")
        sb.AppendLine($"                ""reference"": ""{conditionId}""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          },")

        ' Section 3 - Discharge diagnosis
        sb.AppendLine("          ""3"": {")
        sb.AppendLine("            ""title"": ""Discharge diagnosis"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""78375-3"",")
        sb.AppendLine("                  ""display"": ""Discharge diagnosis Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(dischargeDiagnosisText)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": [")
        sb.AppendLine("              {")
        sb.AppendLine($"                ""reference"": ""{conditionId}""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          },")

        ' Section 4 - Medications on Discharge
        sb.AppendLine("          ""4"": {")
        sb.AppendLine("            ""title"": ""Medications on Discharge"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""75311-1"",")
        sb.AppendLine("                  ""display"": ""Hospital discharge medications Narrative""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(medicationsText)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""mode"": ""working"",")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 5 - Plan of care
        sb.AppendLine("          ""5"": {")
        sb.AppendLine("            ""title"": ""Plan of care"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""18776-5"",")
        sb.AppendLine("                  ""display"": ""Plan of care""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(planOfCareText)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""mode"": ""working"",")
        sb.AppendLine("            ""entry"": []")
        sb.AppendLine("          },")

        ' Section 7 - Known allergies
        sb.AppendLine("          ""7"": {")
        sb.AppendLine("            ""title"": ""Known allergies"",")
        sb.AppendLine("            ""code"": {")
        sb.AppendLine("              ""coding"": [")
        sb.AppendLine("                {")
        sb.AppendLine("                  ""system"": ""http://loinc.org"",")
        sb.AppendLine("                  ""code"": ""48765-2"",")
        sb.AppendLine("                  ""display"": ""Allergies and adverse reactions""")
        sb.AppendLine("                }")
        sb.AppendLine("              ]")
        sb.AppendLine("            },")
        sb.AppendLine("            ""text"": {")
        sb.AppendLine("              ""status"": ""additional"",")
        sb.AppendLine($"              ""div"": ""<div>{EscapeJson(allergiesText)}</div>""")
        sb.AppendLine("            },")
        sb.AppendLine("            ""entry"": [")
        sb.AppendLine("              {")
        sb.AppendLine($"                ""reference"": ""{allergyId}""")
        sb.AppendLine("              }")
        sb.AppendLine("            ]")
        sb.AppendLine("          }")
        sb.AppendLine("        }")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 2. PATIENT - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildPatientJson(
        patientId As String,
        name As String,
        gender As String,
        birthDate As String,
        nik As String,
        mrNumber As String,
        bpjsNumber As String,
        address As String,
        phone As String,
        organizationId As String,
        organizationName As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Patient"",")
        sb.AppendLine($"  ""id"": ""{patientId}"",")
        sb.AppendLine("  ""identifier"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""usual"",")
        sb.AppendLine("      ""type"": {")
        sb.AppendLine("        ""coding"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("            ""code"": ""MR""")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      },")
        sb.AppendLine($"      ""value"": ""{mrNumber}"",")
        sb.AppendLine("      ""assigner"": {")
        sb.AppendLine($"        ""display"": ""{organizationName}""")
        sb.AppendLine("      }")
        sb.AppendLine("    },")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""type"": {")
        sb.AppendLine("        ""coding"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("            ""code"": ""MB""")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      },")
        sb.AppendLine($"      ""value"": ""{bpjsNumber}"",")
        sb.AppendLine("      ""assigner"": {")
        sb.AppendLine("        ""display"": ""BPJS KESEHATAN""")
        sb.AppendLine("      }")
        sb.AppendLine("    },")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""type"": {")
        sb.AppendLine("        ""coding"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("            ""code"": ""NNIDN""")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      },")
        sb.AppendLine($"      ""value"": ""{nik}"",")
        sb.AppendLine("      ""assigner"": {")
        sb.AppendLine("        ""display"": ""KEMENDAGRI""")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""active"": true,")
        sb.AppendLine("  ""name"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine($"      ""text"": ""{name}""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine($"  ""gender"": ""{gender}"",")
        sb.AppendLine($"  ""birthDate"": ""{birthDate}"",")
        sb.AppendLine("  ""deceasedBoolean"": false,")
        sb.AppendLine("  ""address"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""line"": [")
        sb.AppendLine($"        ""{EscapeJson(address)}""")
        sb.AppendLine("      ],")
        sb.AppendLine($"      ""text"": ""{EscapeJson(address)}"",")
        sb.AppendLine("      ""use"": ""home"",")
        sb.AppendLine("      ""type"": ""both""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""telecom"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""system"": ""phone"",")
        sb.AppendLine($"      ""value"": ""{phone}"",")
        sb.AppendLine("      ""use"": ""mobile""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""managingOrganization"": {")
        sb.AppendLine($"    ""reference"": ""Organization/{organizationId}"",")
        sb.AppendLine($"    ""display"": ""{organizationName}""")
        sb.AppendLine("  }")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 3. ENCOUNTER - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildEncounterJson(
        encounterId As String,
        patientId As String,
        patientName As String,
        practitionerId As String,
        practitionerName As String,
        organizationId As String,
        organizationName As String,
        sepNumber As String,
        startDate As String,
        endDate As String,
        conditionId As String,
        conditionDisplay As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Encounter"",")
        sb.AppendLine($"  ""id"": ""{encounterId}"",")
        sb.AppendLine("  ""identifier"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""system"": ""id_encounter"",")
        sb.AppendLine($"      ""value"": ""{sepNumber}""")
        sb.AppendLine("    },")
        sb.AppendLine("    {")
        sb.AppendLine("      ""system"": ""http://api.bpjs-kesehatan.go.id:8080/Vclaim-rest/SEP/"",")
        sb.AppendLine($"      ""value"": ""{sepNumber}""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""status"": ""finished"",")
        sb.AppendLine("  ""class"": {")
        sb.AppendLine("    ""system"": ""http://hl7.org/fhir/v3/ActCode"",")
        sb.AppendLine("    ""code"": ""IMP"",")
        sb.AppendLine("    ""display"": ""inpatient encounter""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""subject"": {")
        sb.AppendLine($"    ""reference"": ""Patient/{patientId}"",")
        sb.AppendLine($"    ""display"": ""{patientName}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""participant"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""individual"": {")
        sb.AppendLine($"        ""reference"": ""Practitioner/{practitionerId}"",")
        sb.AppendLine($"        ""display"": ""{practitionerName}""")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""period"": {")
        sb.AppendLine($"    ""start"": ""{startDate}"",")
        sb.AppendLine($"    ""end"": ""{endDate}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""serviceProvider"": {")
        sb.AppendLine($"    ""reference"": ""Organization/{organizationId}"",")
        sb.AppendLine($"    ""display"": ""{organizationName}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""diagnosis"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""condition"": {")
        sb.AppendLine($"        ""reference"": ""{conditionId}"",")
        sb.AppendLine($"        ""display"": ""{conditionDisplay}""")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 4. CONDITION - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildConditionJson(
        conditionId As String,
        patientId As String,
        icdCode As String,
        icdDisplay As String,
        onsetDateTime As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Condition"",")
        sb.AppendLine($"  ""id"": ""{conditionId}"",")
        sb.AppendLine("  ""clinicalStatus"": ""active"",")
        sb.AppendLine("  ""verificationStatus"": ""confirmed"",")
        sb.AppendLine("  ""category"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""coding"": [")
        sb.AppendLine("        {")
        sb.AppendLine("          ""system"": ""http://hl7.org/fhir/condition-category"",")
        sb.AppendLine("          ""code"": ""encounter-diagnosis"",")
        sb.AppendLine("          ""display"": ""Encounter Diagnosis""")
        sb.AppendLine("        }")
        sb.AppendLine("      ]")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""code"": {")
        sb.AppendLine("    ""coding"": [")
        sb.AppendLine("      {")
        sb.AppendLine("        ""system"": ""http://hl7.org/fhir/sid/icd-10"",")
        sb.AppendLine($"        ""code"": ""{icdCode}"",")
        sb.AppendLine($"        ""display"": ""{EscapeJson(icdDisplay)}""")
        sb.AppendLine("      }")
        sb.AppendLine("    ],")
        sb.AppendLine($"    ""text"": ""{EscapeJson(icdDisplay)}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""subject"": {")
        sb.AppendLine($"    ""reference"": ""Patient/{patientId}""")
        sb.AppendLine("  },")
        sb.AppendLine($"  ""onsetDateTime"": ""{onsetDateTime}""")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 5. PRACTITIONER - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildPractitionerJson(
        practitionerId As String,
        name As String,
        sipNumber As String,
        nik As String,
        phone As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Practitioner"",")
        sb.AppendLine($"  ""id"": ""{practitionerId}"",")
        sb.AppendLine("  ""identifier"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""system"": ""urn:oid:nomor_sip"",")
        sb.AppendLine($"      ""value"": ""{sipNumber}""")
        sb.AppendLine("    },")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""type"": {")
        sb.AppendLine("        ""coding"": [")
        sb.AppendLine("          {")
        sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v2/0203"",")
        sb.AppendLine("            ""code"": ""NNIDN""")
        sb.AppendLine("          }")
        sb.AppendLine("        ]")
        sb.AppendLine("      },")
        sb.AppendLine($"      ""value"": ""{nik}"",")
        sb.AppendLine("      ""assigner"": {")
        sb.AppendLine("        ""display"": ""KEMDAGRI""")
        sb.AppendLine("      }")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""name"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine($"      ""text"": ""{name}""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""telecom"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""system"": ""phone"",")
        sb.AppendLine($"      ""value"": ""{phone}"",")
        sb.AppendLine("      ""use"": ""work""")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 6. ORGANIZATION - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildOrganizationJson(
        organizationId As String,
        name As String,
        bpjsCode As String,
        kemkesCode As String,
        phone As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Organization"",")
        sb.AppendLine($"  ""id"": ""{organizationId}"",")
        sb.AppendLine("  ""identifier"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""system"": ""urn:oid:bpjs"",")
        sb.AppendLine($"      ""value"": ""{bpjsCode}""")
        sb.AppendLine("    },")
        sb.AppendLine("    {")
        sb.AppendLine("      ""use"": ""official"",")
        sb.AppendLine("      ""system"": ""urn:oid:kemkes"",")
        sb.AppendLine($"      ""value"": ""{kemkesCode}""")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""type"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""coding"": [")
        sb.AppendLine("        {")
        sb.AppendLine("          ""system"": ""http://hl7.org/fhir/organization-type"",")
        sb.AppendLine("          ""code"": ""prov"",")
        sb.AppendLine("          ""display"": ""Healthcare Provider""")
        sb.AppendLine("        }")
        sb.AppendLine("      ]")
        sb.AppendLine("    }")
        sb.AppendLine("  ],")
        sb.AppendLine($"  ""name"": ""{name}"",")
        sb.AppendLine("  ""alias"": [")
        sb.AppendLine($"    ""{name}""")
        sb.AppendLine("  ],")
        sb.AppendLine("  ""telecom"": [")
        sb.AppendLine("    {")
        sb.AppendLine("      ""system"": ""phone"",")
        sb.AppendLine($"      ""value"": ""{phone}"",")
        sb.AppendLine("      ""use"": ""work""")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 7. ALLERGY INTOLERANCE - Mengembalikan String JSON
    ' =====================================================
    Public Function BuildAllergyJson(
        allergyId As String,
        patientId As String,
        allergyText As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""AllergyIntolerance"",")
        sb.AppendLine($"  ""id"": ""{allergyId}"",")
        sb.AppendLine("  ""clinicalStatus"": ""active"",")
        sb.AppendLine("  ""verificationStatus"": ""confirmed"",")
        sb.AppendLine("  ""code"": {")
        sb.AppendLine("    ""coding"": [")
        sb.AppendLine("      {")
        sb.AppendLine("        ""system"": ""http://snomed.info/sct"",")
        sb.AppendLine("        ""code"": ""716186003"",")
        sb.AppendLine("        ""display"": ""No known allergy""")
        sb.AppendLine("      }")
        sb.AppendLine("    ]")
        sb.AppendLine("  },")
        sb.AppendLine("  ""patient"": {")
        sb.AppendLine($"    ""reference"": ""Patient/{patientId}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""note"": [")
        sb.AppendLine("    {")
        sb.AppendLine($"      ""text"": ""{EscapeJson(allergyText)}""")
        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 8. BUNDLE - Menggabungkan Semua Resource ke dalam Bundle
    ' =====================================================
    Public Function BuildBundleJson(
        bundleId As String,
        sepNumber As String,
        compositionJson As String,
        patientJson As String,
        encounterJson As String,
        conditionJson As String,
        practitionerJson As String,
        organizationJson As String,
        allergyJson As String) As String

        Dim sb As New StringBuilder()

        sb.AppendLine("{")
        sb.AppendLine("  ""resourceType"": ""Bundle"",")
        sb.AppendLine($"  ""id"": ""{bundleId}"",")
        sb.AppendLine("  ""meta"": {")
        sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""identifier"": {")
        sb.AppendLine("    ""system"": ""sep"",")
        sb.AppendLine($"    ""value"": ""{sepNumber}""")
        sb.AppendLine("  },")
        sb.AppendLine("  ""type"": ""document"",")
        sb.AppendLine("  ""entry"": [")

        ' Composition Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & compositionJson.Replace(vbCrLf, vbCrLf & "      "))
        sb.AppendLine("    },")

        ' Patient Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & patientJson.Replace(vbCrLf, vbCrLf & "      "))
        sb.AppendLine("    },")

        ' Encounter Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & encounterJson.Replace(vbCrLf, vbCrLf & "      "))
        sb.AppendLine("    },")

        ' Condition Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & conditionJson.Replace(vbCrLf, vbCrLf & "      "))
        sb.AppendLine("    },")

        ' Practitioner Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & practitionerJson.Replace(vbCrLf, vbCrLf & "      "))
        sb.AppendLine("    },")

        ' Organization Entry
        sb.AppendLine("    {")
        sb.AppendLine("      ""resource"": " & organizationJson.Replace(vbCrLf, vbCrLf & "      "))

        ' Allergy Entry (optional)
        If Not String.IsNullOrEmpty(allergyJson) Then
            sb.AppendLine("    },")
            sb.AppendLine("    {")
            sb.AppendLine("      ""resource"": " & allergyJson.Replace(vbCrLf, vbCrLf & "      "))
        End If

        sb.AppendLine("    }")
        sb.AppendLine("  ]")
        sb.AppendLine("}")

        Return sb.ToString()
    End Function

    ' =====================================================
    ' 9. HELPER - Escape karakter khusus JSON
    ' =====================================================
    ' BENAR - Menggunakan With statement
    Private Function EscapeJson(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""

        With text
            Return .Replace("\", "\\") _
               .Replace("""", "\""") _
               .Replace(vbCr, "\r") _
               .Replace(vbLf, "\n") _
               .Replace(vbTab, "\t")
        End With
    End Function

End Class