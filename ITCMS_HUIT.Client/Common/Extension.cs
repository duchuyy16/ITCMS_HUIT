namespace ITCMS_HUIT.Client.Common
{
    public static class Extension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key) ?? default;
            return value != null ? System.Text.Json.JsonSerializer.Deserialize<T>(value) : default;
        }

        //public static string GetUserNameFromJwt(this string jwt)
        //{
        //    if (jwt == null) return string.Empty;

        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = handler.ReadJwtToken(jwt);
        //    var nameClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

        //    if (nameClaim != null)
        //        return nameClaim.Value;
        //    else
        //        return string.Empty;

        //}
    }
}
