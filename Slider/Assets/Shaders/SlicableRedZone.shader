Shader "Custom/SlicableRedZone"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _SliceColor ("Slice color", Color) = (1,1,1,1)
        _SliceWidth("Slice line width", Range(0, 0.1)) = 0.05
        _Point1("Point 1", Vector) = (0, 0, 0, 0)
        _Point2("Point 1", Vector) = (0, 0, 0, 0)
        
        _RedZoneColor ("Red Zone color", Color) = (1,1,1,1)
        _RedZoneWidth("Red line width", Range(0, 2)) = 0.05
        _RedZonePoint1("Red Zone Point 1", Vector) = (0, 0, 0, 0)
        _RedZonePoint2("Red ZonePoint 1", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        fixed4 _Color;
        fixed4 _SliceColor;
        fixed _SliceWidth;

        fixed4 _RedZoneColor;
        fixed _RedZoneWidth;

        fixed3 _Point1;
        fixed3 _Point2;

        fixed3 _RedZonePoint1;
        fixed3 _RedZonePoint2;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed x0 = IN.worldPos.x;
            fixed z0 = IN.worldPos.z;

            fixed x1 = _Point1.x;
            fixed z1 = _Point1.z;

            fixed x2 = _Point2.x;
            fixed z2 = _Point2.z;

            fixed redZoneX1 = _RedZonePoint1.x;
            fixed redZoneZ1 = _RedZonePoint1.z;

            fixed redZoneX2 = _RedZonePoint2.x;
            fixed redZoneZ2 = _RedZonePoint2.z;

            fixed res = abs((z2 - z1) * x0 - (x2 - x1) * z0 + x2 * z1 - z2 * x1);
            res /= sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1));

            fixed redZoneRes = abs((redZoneZ2 - redZoneZ1) * x0 - (redZoneX2 - redZoneX1) * z0 + redZoneX2 * redZoneZ1 - redZoneZ2 * redZoneX1);
            redZoneRes /= sqrt((redZoneZ2 - redZoneZ1) * (redZoneZ2 - redZoneZ1) + (redZoneX2 - redZoneX1) * (redZoneX2 - redZoneX1));

            float4 tex = tex2D(_MainTex, IN.uv_MainTex);

            if( res <= _SliceWidth / 2 && res >= -_SliceWidth / 2)
            {
                o.Albedo = _SliceColor;
            }
            else if(redZoneRes <= _RedZoneWidth / 2 && redZoneRes >= -_RedZoneWidth / 2)
            {
                o.Albedo = _RedZoneColor;
            }
            else
            {
                o.Albedo = tex.rgb;
            }
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
