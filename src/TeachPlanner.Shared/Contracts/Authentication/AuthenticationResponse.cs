﻿using TeachPlanner.Shared.Contracts.Teachers;

namespace TeachPlanner.Shared.Contracts.Authentication;

public record AuthenticationResponse(string Token, DateTime Expiration, string RefreshToken);