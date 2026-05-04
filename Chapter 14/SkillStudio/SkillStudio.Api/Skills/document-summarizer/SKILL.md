---
name: document-summarizer
description: Summarize uploaded text, markdown, or notes into clear key points, action items, and a concise overview. Use when the user provides document content and wants a compact, readable summary.
---

# Document Summarizer

Use this skill when the input is:
- Plain text
- Markdown
- Notes
- Meeting summaries
- Documentation drafts

Do not use this skill when:
- The user asks for code review
- The user asks for JWT or security claim analysis
- The user wants a conversational assistant without a document

## Goals

- Produce a concise summary
- Identify major themes
- Extract action items when present
- Preserve important technical meaning
- Avoid copying large portions verbatim

## Output format

Return:
1. A two- to four-sentence summary
2. Key points as bullets
3. Action items as bullets, if present
4. Open questions, if present

## Guidance

- Prefer clarity over compression
- If the content is technical, retain terms that matter
- If no action items exist, state that none were clearly identified
- If the content is already brief, avoid over-summarizing

## Example input

A markdown file describing a Blazor WebAssembly project, setup steps, and future enhancements.

## Example output style

Summary:
This document outlines a Blazor WebAssembly project and explains its setup, architecture, and next steps. It focuses on practical implementation details and identifies areas for future improvement.

Key points:
- Project uses Blazor WebAssembly with an ASP.NET Core API
- Authentication is handled separately from the client
- Future work includes better caching and UI polish

Action items:
- Add error handling for failed API calls
- Document deployment steps