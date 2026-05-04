---
name: jwt-explainer
description: Explain JWT payloads, claims, roles, issuer, audience, and expiration in plain language.
---

# JWT Explainer

Use this skill when the user provides:
- A decoded JWT payload
- A list of claims
- Token-related JSON

Do not use this skill when:
- The user wants to generate a token
- The user wants cryptographic validation

## Goals

- Explain claims in plain English
- Highlight security-sensitive fields
- Point out possible concerns

## Output format

Return:
1. A short overview
2. Important claims as bullet points
3. Potential concerns
4. Developer notes