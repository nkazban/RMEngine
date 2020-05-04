#version 440

#define MAX_STEPS 100
#define MAX_DIST 100.
#define SURF_DIST .001

uniform vec2 Resolution;
uniform vec3 cameraPosition;
uniform vec3 spherePosition;

float PI=3.14159265;

float plane(vec3 p)
{
  return p.y;
}

float sphere(vec3 p, vec4 s) {
    return length(p - s.xyz) - s.w;
}
float distance(vec3 p)
{
  return min(plane(p), sphere(p, vec4(spherePosition, 1)));
}

vec3 GetNormal(vec3 p) {
	float d = distance(p);
    vec2 e = vec2(.01, 0);
    
    vec3 n = d - vec3(
        distance(p-e.xyy),
        distance(p-e.yxy),
        distance(p-e.yyx));
    
    return normalize(n);
}
float RayMarch(vec3 ro, vec3 rd) {
	float dO=0.;
    
    for(int i=0; i<MAX_STEPS; i++) {
    	vec3 p = ro + rd*dO;
        float dS = distance(p);
        dO += dS;
        if(dO>MAX_DIST || dS<SURF_DIST) 
            break;
    }
    
    return dO;
}
float GetLight(vec3 p) {
    vec3 lightPos = vec3(0, 5, 6);
    vec3 l = normalize(lightPos-p);
    vec3 n = GetNormal(p);
    
    float dif = clamp(dot(n, l), 0., 1.);
    float d = RayMarch(p+n*SURF_DIST*2., l);
    if(d<length(lightPos-p)) dif *= .1;
    
    return dif;
}

in vec4 gl_FragCoord;
out vec4 color;
void main(void)
{
    vec3 col = vec3(1, 0, 0);
    vec2 uv = (gl_FragCoord.xy - Resolution / 2) / Resolution.y;
    vec3 rayDirection = normalize(vec3(uv.x, uv.y, 1.0));
    vec3 rayOrigin = cameraPosition;//vec3(0, 1, -1);

    float d = RayMarch(rayOrigin, rayDirection);
    
    vec3 p = rayOrigin + rayDirection * d;
    
    float dif = GetLight(p);
    col = vec3(dif);
    
    color=vec4(col,1); //background color
}