Shader "Custom/testShader4"{
	Properties{
	  _Color("Color", Color) = (1,1,1,1)								//����ɫ��ֵ
	  _MainTex("Albedo (RGB)", 2D) = "white" {}							//2D������ֵ
	  _Shininess("Shininess ", Range(0,10)) = 10						//���淴��ϵ��
	  _Ambient("Ambient", range(0, 1)) = 0.01                            //������ϵ��
	}
		SubShader{
		  CGPROGRAM
		  #pragma surface surf BlingPhong									//������ɫ������ָ��
		  sampler2D _MainTex;										//2D��������
		  fixed4 _Color;												//����ɫ����
		  float _Shininess;											//���淴��ϵ������
		  float _Ambient;
		  struct Input {
			float2 uv_MainTex;											//uv��������
		  };
		  float4 LightingBlingPhong(SurfaceOutput s, float3 lightDir,half3 viewDir, half atten) {//����ģ�ͺ���
			float4 c;
			float diffuseF = max(0,dot(s.Normal,lightDir));						//����������ǿ��
			float specF;
			float3 H = normalize(lightDir + viewDir);						//������������ߵİ�����
			float specBase = max(0,dot(s.Normal,H));					//���㷨����������ĵ��
			specF = pow(specBase,_Shininess);						//���㾵�淴��ǿ��
			c.rgb = s.Albedo * _LightColor0 * diffuseF * atten + _LightColor0 * specF + _Ambient * _LightColor0;
			//�����������뾵�淴���������չ�����ɫ
			c.a = s.Alpha;
			return c;												//�������չ�����ɫ
		  }
		  void surf(Input IN, inout SurfaceOutput o) {					//������ɫ������
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;			//����UV�����������ȡ��ɫ
			o.Albedo = c.rgb;										//������ɫ
			o.Alpha = c.a;											//����͸����
		  }
		  ENDCG
	  }
		  FallBack "Diffuse"											//������ɫ��
}
