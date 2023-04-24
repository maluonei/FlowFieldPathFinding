Shader "Custom/testShader3"{
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Amount("Extrusion Amount", Range(0,0.1)) = 0.1
	}
		SubShader{
			CGPROGRAM
			#pragma surface surf Lambert vertex:vert
			struct Input {
				float2 uv_MainTex;
			};
			float _Amount;
			sampler2D _MainTex;
			void vert(inout appdata_base v) {						//����任����
				v.vertex.xyz += v.normal * _Amount;			//ͨ�����߼�ѹʵ�ֳ�����Ч��
			}
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			}
			ENDCG
		}
			Fallback "Diffuse"
}
//Shader "Custom/testShader3" {
//	Properties{
//	  _MainTex("Texture", 2D) = "white" {}						//2D������ֵ
//	  _Amount("Extrusion Amount", Range(0,0.1)) = 0.1			//����ϵ����ֵ
//	}
//		SubShader{
//		  CGPROGRAM
//		  #pragma surface surf Lambert vertex:vert					//������ɫ������ָ��
//		  struct Input {										//Input�ṹ��
//			float2 uv_MainTex;									//uv��������
//		  };
//		  float _Amount;										//��������ϵ������
//		  sampler2D _MainTex;								//����2D����
//		  void vert(inout appdata_base v) {						//����任����
//			v.vertex.xyz += v.normal * _Amount;			//ͨ�����߼�ѹʵ�ֳ�����Ч��
//		  }
//		  void surf(Input IN, inout SurfaceOutput o) {				//������ɫ������
//			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;	//��������ȡ��ɫΪ��������ɫ��ֵ
//		  }
//		  ENDCG
//	  }
//		  Fallback "Diffuse"												//������ɫ��
//}
//
