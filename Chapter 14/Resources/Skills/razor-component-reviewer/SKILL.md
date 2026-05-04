---
name: razor-component-reviewer
description: Review Razor components for readability, accessibility, maintainability, and Blazor best practices. Use when the user provides Razor markup or a Blazor component and wants feedback or improvement suggestions.
---

# Razor Component Reviewer

Use this skill when the user provides:
- A `.razor` component
- Razor markup with `@code`
- Blazor UI code that needs review

Do not use this skill when:
- The input is a JWT payload
- The input is general prose or notes
- The user wants a summary rather than code-focused feedback

## Goals

- Identify readability issues
- Suggest accessibility improvements
- Point out maintainability concerns
- Recommend Blazor-friendly patterns
- Keep feedback practical and specific

## Output format

Return:
1. A short assessment
2. Strengths
3. Suggested improvements
4. Revised sample snippet if helpful

## Guidance

- Comment on naming, structure, and separation of concerns
- Note missing labels, semantic elements, or ARIA concerns
- Suggest component extraction when markup grows too large
- Do not rewrite the entire component unless necessary
- Favor incremental improvements over dramatic redesign

## Example input

A Blazor component with repeated markup, unlabeled inputs, and inline logic.

## Example output style

Assessment:
The component works, but it mixes UI structure and logic in a way that may become harder to maintain.

Strengths:
- Clear intent
- Uses standard Blazor bindings

Suggested improvements:
- Add labels for form controls
- Move repeated markup into a child component
- Reduce inline logic in the markup
- Add loading and error states if data is asynchronous