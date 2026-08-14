import type * as React from "react"
import { cva, type VariantProps } from "class-variance-authority"

import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"

function InputGroup({ className, ...props }: React.ComponentProps<"div">) {
  return (
    <div
      data-slot="input-group"
      className={cn(
        "group/input-group relative flex h-15 w-full items-center overflow-hidden rounded-xl border border-white/15 bg-white/[0.035] text-amber-400 transition-[border-color,box-shadow,background-color]",
        "focus-within:border-amber-400/70 focus-within:bg-amber-400/[0.05] focus-within:ring-3 focus-within:ring-amber-400/10",
        "has-[[data-slot=input-group-control]:disabled]:pointer-events-none has-[[data-slot=input-group-control]:disabled]:opacity-50",
        className
      )}
      {...props}
    />
  )
}

function InputGroupInput({ className, ...props }: React.ComponentProps<"input">) {
  return (
    <Input
      data-slot="input-group-control"
      className={cn(
        "h-full flex-1 rounded-none border-0 bg-transparent px-3 text-sm text-white shadow-none placeholder:text-white/40 focus-visible:border-0 focus-visible:ring-0",
        className
      )}
      {...props}
    />
  )
}

const inputGroupAddonVariants = cva(
  "flex h-full shrink-0 items-center justify-center text-current [&>svg]:size-5",
  {
    variants: {
      align: {
        "inline-start": "order-first pl-5",
        "inline-end": "order-last pr-4",
      },
    },
    defaultVariants: {
      align: "inline-start",
    },
  }
)

function InputGroupAddon({
  className,
  align = "inline-start",
  onClick,
  ...props
}: React.ComponentProps<"div"> & VariantProps<typeof inputGroupAddonVariants>) {
  return (
    <div
      data-slot="input-group-addon"
      data-align={align}
      className={cn(inputGroupAddonVariants({ align }), className)}
      onClick={(event) => {
        if ((event.target as HTMLElement).closest("button")) return
        event.currentTarget.parentElement
          ?.querySelector<HTMLInputElement>("[data-slot=input-group-control]")
          ?.focus()
        onClick?.(event)
      }}
      {...props}
    />
  )
}

function InputGroupButton({ className, ...props }: React.ComponentProps<typeof Button>) {
  return (
    <Button
      data-slot="input-group-button"
      type="button"
      variant="ghost"
      size="icon-sm"
      className={cn(
        "rounded-md text-white/45 hover:bg-transparent hover:text-amber-400",
        className
      )}
      {...props}
    />
  )
}

export { InputGroup, InputGroupAddon, InputGroupButton, InputGroupInput }
