Shader "Custom/DynamicHeadCutoff" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        // Nilai ini sekarang menjadi jarak dari pivot/titik pusat bawah objek 3D Anda
        _CutoffHeight ("Cutoff Height (Local Y)", Float) = 0.5 
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off 

        CGPROGRAM
        // Menambahkan 'vertex:vert' untuk mengambil data koordinat lokal
        #pragma surface surf Standard vertex:vert fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _CutoffHeight;

        struct Input {
            float2 uv_MainTex;
            float3 localPos; // Menggunakan koordinat lokal, bukan dunia lagi
        };

        // Fungsi Vertex untuk membaca posisi murni dari model 3D Anda
        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.localPos = v.vertex.xyz; // Menyimpan posisi lokal asli mesh
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // LOGIKA DINAMIS: Pemotongan sekarang mengunci pada badan objek
            if (IN.localPos.y > _CutoffHeight) {
                discard;
            }

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}